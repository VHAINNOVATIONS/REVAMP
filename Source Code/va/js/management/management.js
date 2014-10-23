//MANAGEMENT namespace
if(management === undefined)
{
    var management = {
	
		//edit grid
		editgrid: {
		
			//editrow
			editrow: function(obj)
			{
				return false;
			},
			
			//cancel editrow
			canceleditrow: function(obj)
			{
				return false;
			}
			
		}
		
	};
}

// Notify ScriptManager that this is the end of the script.
if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();