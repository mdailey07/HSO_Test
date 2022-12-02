using HSO.API.Controllers;

namespace HSO.API.Test.HomeTestControllerTest;

public class Post
{
    private HomeTestController _sut;
    
    [SetUp]
    public void Setup()
    {
        _sut = new HomeTestController();
    }

    [Test]
    public void regular_string_with_environment_new_line()
    {   
        // Arrange
        const string stringTest = @"'Patient Name','SSN','Age','Phone Number','Status'
'Prescott, Zeke','542-51-6641',21,'801-555-2134','Opratory=2,PCP=1'
'Goldstein, Bucky','635-45-1254',42,'435-555-1541','Opratory=1,PCP=1'
'Vox, Bono','414-45-1475',51,'801-555-2100','Opratory=3,PCP=2'";
       
        const string expectedResult = @"[Patient Name] [SSN] [Age] [Phone Number] [Status] 
[Prescott, Zeke] [542-51-6641] [21] [801-555-2134] [Opratory=2,PCP=1] 
[Goldstein, Bucky] [635-45-1254] [42] [435-555-1541] [Opratory=1,PCP=1] 
[Vox, Bono] [414-45-1475] [51] [801-555-2100] [Opratory=3,PCP=2]"; 
        
        // Act
        var result = _sut.CustomParse(stringTest);
        
        // Assert
        Assert.AreEqual(result,expectedResult);
    }
    
        
    [Test]
    public void regular_string_without_environment_new_line()
    {   
        // Arrange
        const string stringTest = "'Patient Name','SSN','Age','Phone Number','Status'" +
                                  "'Prescott, Zeke','542-51-6641',21,'801-555-2134','Opratory=2,PCP=1'" +
                                  "'Goldstein, Bucky','635-45-1254',42,'435-555-1541','Opratory=1,PCP=1'" +
                                  "'Vox, Bono','414-45-1475',51,'801-555-2100','Opratory=3,PCP=2'";
        const string expectedResult = @"[Patient Name] [SSN] [Age] [Phone Number] [Status] 
[Prescott, Zeke] [542-51-6641] [21] [801-555-2134] [Opratory=2,PCP=1] 
[Goldstein, Bucky] [635-45-1254] [42] [435-555-1541] [Opratory=1,PCP=1] 
[Vox, Bono] [414-45-1475] [51] [801-555-2100] [Opratory=3,PCP=2]"; 
        
        // Act
        var result = _sut.CustomParse(stringTest);
        
        // Assert
        Assert.AreEqual(result,expectedResult);
    }
}