<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
 xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
 xmlns:gl="galaxies" xmlns:st="stars" xmlns:pl='planets' xmlns:sa='satelite' >

  <xsl:template match="/">
    <html>
      <head>
        <title>Interneto technologijos</title>
      </head>
      <body>
        <xsl:apply-templates select="/gl:galaxy"/>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="gl:galaxy">
    <h1>
      <xsl:value-of select="@name"/>
    </h1>
    <p>
      Star quantity: 
      <xsl:apply-templates />
    </p>
  </xsl:template>
  
  <xsl:template match="/gl:galaxy/gl:star">
    <h2 style="padding-left:5em">
      <xsl:value-of select="@name"/>
    </h2>
    <p style="padding-left:5em"> 
      Mass: <xsl:value-of select="st:mass"/> * 10^23
      <br/>
      Planet quantity: <xsl:value-of select="st:planet_quantity"/>
      <br/>
      Planets: <xsl:apply-templates select="pl:planet"/>
    </p>
  </xsl:template>

  <xsl:template match="/gl:galaxy/gl:star/pl:planet">
    <h3 style="padding-left:10em">
      <xsl:number value="position()" format="1. "/>
      <xsl:value-of select="@name"/>
    </h3>
    <p style="padding-left:10em">
      Type: <xsl:value-of select="@type"/>
      <br/>
      <xsl:if test="@discovered">
        Discover date: <xsl:value-of select="@discovered"/>
        <br/>
      </xsl:if>
      <xsl:if test="pl:discovered">
        Discovered by: <xsl:value-of select="pl:discoverer"/>
        <br/>
      </xsl:if>
      Mass: <xsl:value-of select="pl:mass"/> * 10^23
      <br/>
      Satelite quantity: <xsl:value-of select="pl:satelite_quantity"/>
    </p>

    <xsl:if test="sa:satelite">
      <xsl:apply-templates select="sa:satelite">
        <xsl:sort select="@name"/>
      </xsl:apply-templates>
    </xsl:if>

    <xsl:if test="pl:continent_quantity">
      <p style="padding-left:10em">
        Continent quantity: <xsl:value-of select="pl:continent_quantity"/>
        <br/>
        Continents:
        <br/>
        <xsl:for-each select="pl:continent">
          <xsl:sort select="@name"/>
          <xsl:number value="position()" format="1. "/>
          <xsl:value-of select="@name"/>
          <br/>
        </xsl:for-each>
      </p>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="/gl:galaxy/gl:star/pl:planet/sa:satelite">
    <h4 style="padding-left:15em">
      <xsl:number value="position()" format="1. "/>
      <xsl:value-of select="@name"/>
    </h4>
    <p style="padding-left:15em">
      Mass: <xsl:value-of select="sa:mass"/> * 10^23
    </p>
  </xsl:template>
  
</xsl:stylesheet>