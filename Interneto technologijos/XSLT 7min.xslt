<?xml version="1.0" encoding="UTF-8"?>
  <xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
    <xsl:template match='/'>
      <table border="1">
       <xsl:apply-templates select='//@*'/>
      </table>
    </xsl:template>
    <xsl:template match="@*">
      <tr>
        <td>
          <xsl:value-of select="."/>
        </td>
      </tr>
    </xsl:template>
  </xsl:stylesheet>