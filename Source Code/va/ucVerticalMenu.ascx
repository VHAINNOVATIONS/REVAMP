<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucVerticalMenu.ascx.cs" Inherits="ucVerticalMenu" %>

<div id="divVerticalMenu" runat="server" class="tree-back"></div>

<script type="text/javascript">
    var showSOAPReport = function(opts)
    {
        winrpt.showReport('patSOAPPRpt', [opts.patientID, opts.encounterID, opts.treatmentID]);
    };

    var showAssessmentReport = function(opts)
    {
        winrpt.showReport('patAssessmentsRpt', [opts.encounterID, opts.encounterIntakeID, opts.treatmentID]);
    };
</script>

