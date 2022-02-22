# Wmda - Assignment - Bhavin
## Setup:

1. Open WMDAApi.sln in visual studio. 
2. Update connection string in appsettings.json to point to correct Sql Server instance as per your machine setup. 
3. Open Package Manager Console by click on Tool Menu then NuGet Package Manager then Package Manager Console.
4. Run the following command on Package Manager Console
	> Update-Database
5. Build solution. 
6. Run test cases
7. Run the project

## Additional notes:
- Small observation - Match Engine 1 example.json has a missing double quote after the word dateOfBirth. 
  > `{ "patient": { "forename": "", "surname": "", "dateOfBirth:"", "diseaseType": "" } }`
- In Patient class. We need to hide PatientId from swagger UI. Where I have used JsonIgnore which very handy to achieve this. It wondoesn't have any side effect in current implementation as we don't have any other methods like update patient. Though if we have requirement to hide it conditionally we can implement different trick. 
