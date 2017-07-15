{-# LANGUAGE OverloadedStrings #-}
module Http where
 
import Network.Wreq as Wreq
import Network.HTTP.Client
import Control.Exception as E
import Control.Lens
import Data.Either
import Data.ByteString.Char8 as Char8
import Data.ByteString.Lazy as LBStr

httpGet :: String -> IO String
httpGet url = do
    let opts = defaults & header "Accept" .~ ["application/m-expr+map"]
    r <- (Right <$> getWith opts url) `E.catch` httpExceptionHandler
    return $ getValue r
    where 
        getValue :: Either String (Response LBStr.ByteString) -> String
        getValue (Left statusMessage) = statusMessage
        getValue (Right response) = Char8.unpack $ LBStr.toStrict (response ^. Wreq.responseBody)

httpExceptionHandler (StatusCodeException s _ _) = return $ Left $ Char8.unpack (s ^. statusMessage)

httpPost :: String -> String -> IO Int
httpPost url message = do
    let opts = defaults & header "Content-Type" .~ ["application/m-expr+map"]
    r <- postWith opts url $ Char8.pack message
    let result = r ^. Wreq.responseStatus . statusCode
    return result

baseUrl = "http://tictactoe.homedir.eu/game/"
buildUrl :: String -> String -> String
buildUrl gameId player =  
    baseUrl ++
    gameId ++ 
    "/player/" ++ 
    player