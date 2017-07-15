<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
 xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
 xmlns:pl='planet' xmlns:ctn='continent' xmlns:ctr='country' xmlns:ca='city' xmlns:reg='region'>

  <xsl:template match="/">
    <html>
      <head>
        <title>Interneto technologijos</title>
      </head>
      <body>
        <xsl:apply-templates select="pl:Planet"/> <!--5-->
      </body>
    </html>
  </xsl:template>

  <xsl:template match="pl:Planet">
    <h1>
      <xsl:value-of select="@name"/> <!--6/7-->
    </h1>
    <p>
      Population, Continent quantity:
      <xsl:apply-templates> <!--4, pagal nutilejima paima populacija, kontinentu kieki ir pacius kontinentus-->
        <xsl:sort select="@name"/> <!--2/7-->
      </xsl:apply-templates>
    </p>
  </xsl:template>

  <xsl:template match="/pl:Planet/ctn:Continent"> <!--1-->
    <h2 style="padding-left:5em">
      <xsl:value-of select="@name"/> <!--6/7-->
    </h2>
    <p style="padding-left:5em">
      <xsl:if test="ctn:Population > 0"> <!--7-->
        Population: <xsl:value-of select="ctn:Population"/>
        <br/>
      </xsl:if>
      <xsl:if test="ctn:Country_quantity > 0">
        Country quantity: <xsl:value-of select="ctn:Country_quantity"/>
        <br/>
      </xsl:if>
      <xsl:apply-templates select="ctr:Country"/> <!--5-->
    </p>
  </xsl:template>

  <xsl:template match="/pl:Planet/ctn:Continent/ctr:Country"> <!--1-->
    <h3 style="padding-left:10em">
      <xsl:number value="position()" format="1. "/>
      <xsl:value-of select="@name"/>
    </h3>
    <p style="padding-left:10em">
      Type: <xsl:value-of select="@type"/>
      <br/>
      Population: <xsl:value-of select="ctr:Population"/>
      <br/>
      Capital:
      <xsl:apply-templates select="ca:Capital"> <!--5-->
        <xsl:sort select="@name"/> <!--2-->
      </xsl:apply-templates>
    </p>
    </xsl:template>

  <xsl:template match="/pl:Planet/ctn:Continent/ctr:Country/ca:Capital"> <!--1-->
    <h4 style="padding-left:15em">
      <xsl:number value="position()" format="1. "/> <!--3-->
      <xsl:value-of select="@name"/>
    </h4>
    <p style="padding-left:15em">
      Population: <xsl:value-of select="ca:Population"/>
      <xsl:for-each select="reg:Region"> <!--7-->
        <xsl:sort select="@name"/> <!--2-->
        <h4 style="padding-left:20em">
          <xsl:number value="position()" format="1. "/> <!--3-->
          <xsl:value-of select="@name"/>
        </h4>
        <p style="padding-left:20em">
          <xsl:if test="reg:Population">
            Population: <xsl:value-of select="reg:Population"/>
            <br/>
          </xsl:if>
          Area: <xsl:value-of select="reg:Area"/>
        </p>
      </xsl:for-each>
    </p>
  </xsl:template>
</xsl:stylesheet>