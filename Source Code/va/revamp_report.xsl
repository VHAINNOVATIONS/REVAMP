<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/report">
    <html>
      <head>
        <title>Revamp Report</title>
        <style>

          body {
          font-family: sans-serif;
          font-size: 11px;
          }

          .report-info {
          display: block;
          padding: 2px 6px;
          background: #f1f1f1;
          margin: 10px 0;
          }

          .tbl-generic {
          width: 100%;
          border-collapse: collapse;
          }

          .tbl-generic th,
          .tbl-generic td {
          vertical-align: top;
          border: 1px solid #376ba0;
          padding: 2px 6px;
          }

          .tbl-generic td {
          vertical-align: middle;
          }

          .tbl-generic th {
          background: #376ba0;
          color: #fff;
          }

          .tbl-generic input[type="checkbox"],
          .tbl-generic input[type="radio"] {
          margin-right: 8px;
          }

          .tbl-generic thead tr th:nth-child(1) {
          width: 40%;
          }

          .tbl-generic tbody tr td:nth-child(1) {
          color: #376ba0;
          font-weight: bold;
          }

          .tbl-generic tbody tr td:nth-child(4) {
          font-family: monospace;
          padding: 10px;
          }

          .tbl-generic tbody tr:nth-child(2n+2) td {
          background: #ecf3f5;
          }

        </style>
      </head>
      <body>
        <h1>Revamp Report</h1>
        <span class="report-info">
          (Auto-generated on:
          <xsl:for-each select="info">
            <xsl:value-of select="date_generated" />
          </xsl:for-each>)
        </span>
        <table class="tbl-generic">
          <thead>
            <tr>
              <th>Description</th>
              <th>Data Entry</th>
              <th>Result</th>
              <th>Query</th>
            </tr>
          </thead>
          <tbody>
            <xsl:for-each select="results/result">
              <tr>
                <td>
                  <xsl:value-of select="description" />
                </td>
                <td>
                  <xsl:value-of select="data_entry" />
                </td>
                <td>
                  <xsl:value-of select="value" />
                </td>
                <td>
                  <xsl:value-of select="query" />
                </td>
              </tr>
            </xsl:for-each>
          </tbody>
        </table>
        <br />
        <hr />
        <br />
        <table>
          <tr>
            <td style="vertical-align: top; width: 48%;">
              <h3>Race distribution</h3>
              <table class="tbl-generic">
                <thead>
                  <tr>
                    <th style="width: 70%;">Race</th>
                    <th>Count</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:for-each select="races/race">
                    <tr>
                      <td>
                        <xsl:value-of select="name" />
                      </td>
                      <td>
                        <xsl:value-of select="value" />
                      </td>
                    </tr>
                  </xsl:for-each>
                </tbody>
              </table>
            </td>
            <td style="vertical-align: top; width: 4%;"></td>
            <td style="vertical-align: top; width: 48%;">
              <h3>Ethnicity distribution</h3>
              <table class="tbl-generic">
                <thead>
                  <tr>
                    <th style="width: 70%;">Ethnicity</th>
                    <th>Count</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:for-each select="ethnicities/ethnicity">
                    <tr>
                      <td>
                        <xsl:value-of select="name" />
                      </td>
                      <td>
                        <xsl:value-of select="value" />
                      </td>
                    </tr>
                  </xsl:for-each>
                </tbody>
              </table>
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>