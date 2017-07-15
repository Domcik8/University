module Main where

import Game
import AI
import Types
import MExpression.Parse
import MExpression.Serialize
import Test.Hspec
import Control.Exception (evaluate)

main :: IO ()
main = hspec $ do
  describe "MExpression.Parse" $ do
    it "returns [] if m-expression is empty" $ do
      parse "m[]" `shouldBe` []

    it "returns [Move {x = 0, y = 2, player 'x'}]" $ do
      parse "m[\"0\";  m[\"x\"; 0;   \"y\";   2;   \"v\";   \"x\"]]" `shouldBe` [Move 0 2 'x']

    it "returns [Move {x = 2, y = 0, player 'x'}] when parameters are switched" $ do
      parse "m[\"0\";  m[\"y\"; 0;   \"v\";   \"x\";   \"x\";   2]]" `shouldBe` [Move 2 0 'x']

  describe "MExpression.Serialize" $ do
    it "returns 'm[]' if no moves in array" $ do
      serialize [] `shouldBe` "m[]"

    it "returns 'm[\"0\";m[\"x\"; 2;\"y\";2;\"v\";\"x\"]]' after serialization" $ do
      serialize [Move 2 2 'x'] `shouldBe` "m[\"0\";m[\"x\";2;\"y\";2;\"v\";\"x\"]]"

  describe "MExpression" $ do
    it "returns True if parsed and serialized string equals to original" $ do
      (serialize (parse "m[\"0\";m[\"x\";2;\"y\";2;\"v\";\"x\"]]")) == "m[\"0\";m[\"x\";2;\"y\";2;\"v\";\"x\"]]" `shouldBe` True

  describe "AI / Game" $ do
    it "returns True if current spot is obligative to defend" $ do
      isAMustSpot [Move 2 2 'x', Move 1 1 'x'] (0, 0) 'o' `shouldBe` True

    it "return True if current spot is obligative to defend" $ do
      isAMustSpot [Move 2 2 'x', Move 1 1 'x'] (1, 2) 'o' `shouldBe` False

    it "returns Move 5 2 'x' if no obligative moves can be made" $ do
      getMustMove [] [] 'x' `shouldBe` Move 5 2 'x'

    it "returns Move 1 1 'x' as a obligative move to do" $ do
      getMustMove [Move 2 2 'o', Move 1 1 'o'] (getFreeSpots [Move 2 2 'o', Move 1 1 'o']) 'x' `shouldBe` Move 0 0 'x'

    it "returns a corner spot when atleast one is awailable and there are no obligative spots to defend" $ do
      attack [Move 1 1 'x'] (getFreeSpots [Move 1 1 'x']) 'o' `shouldBe` Move 0 0 'o'

    it "return true if game is already over" $ do
      isGameOver [] `shouldBe` False

    it "return true if game is already over" $ do
      isGameOver [Move 0 0 'x',
                  Move 1 1 'x',
                  Move 2 2 'x'] `shouldBe` True

    it "return true if game is already over" $ do
      isGameOver [Move 2 0 'x',
                  Move 1 1 'x',
                  Move 0 2 'x'] `shouldBe` True