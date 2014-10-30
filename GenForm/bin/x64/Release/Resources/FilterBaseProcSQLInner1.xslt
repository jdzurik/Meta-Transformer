<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
	<xsl:output method="text" standalone="yes"/>
	<xsl:preserve-space elements="row"/>
	<xsl:param name="FilterGuid"></xsl:param>
	
    <xsl:template match="/TableSet" name="InnerSQL">
		<xsl:choose>
			<xsl:when test="Filters/FilterSet[ItemGuid=$FilterGuid]/IsAdvanced='false'">
				
FROM [<xsl:value-of select="TableName"/>]
<xsl:if test="Filters/FilterSet[ItemGuid=$FilterGuid]/Where/FilterWhereSet">
	WHERE <xsl:call-template name="tWhere"></xsl:call-template>
</xsl:if>
			</xsl:when>
			<xsl:when test="Filters/FilterSet[ItemGuid=$FilterGuid]/IsAdvanced='true'">
-- Advanced area

<xsl:value-of select="Filters/FilterSet/FilterQueryInner"/>

-- End Advanced area
			</xsl:when>
		</xsl:choose>
    </xsl:template> 

	<xsl:template name="tWhere" match="/Filters">
		<xsl:for-each select="Filters/FilterSet[ItemGuid=$FilterGuid]/Where/FilterWhereSet">
			<xsl:value-of select="Pre"/>[<xsl:value-of select="ColumnName"/>]<xsl:value-of select="Operator"/><xsl:value-of select="WhereValue"/><xsl:value-of select="Post"/>			
		</xsl:for-each>
	</xsl:template>

	
	
</xsl:stylesheet>
