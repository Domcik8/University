<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
  <xsl:template match='/lentele'>
    <xsl:for-each select="stulpelis">
      <xsl:sort select="Vardas"/>
      <p>
        <xsl:number value="position()" format="1. "/>
        <xsl:value-of select="Vardas"/>
        <xsl:value-of select="Amzius"/>
      </p>
    </xsl:for-each>
    <p>
      <br></br>
    </p>
    <xsl:for-each select="stulpelis">
      <xsl:sort select="Amzius" data-type="number"/>
      <p>
        <xsl:number value="position()" format="1. "/>
        <xsl:value-of select="Vardas"/>
        <xsl:value-of select="Amzius"/>
      </p>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>