using AbstractLL;

using CleanArc.Models;

namespace CleanArc.Parser
{
    internal class Parser : AbstractParser<SolutionModel>
    {
        private static Tag[][] _RHS = new Tag[][]
        {
            //  0. <Start> -> <Text> @Done "#"
            new Tag[]{ Tag.MainText, Tag._Done, Tag.ENDMARK, },
            //  1. <Text> -> <Declaration> <Declaration'> 
            new Tag[]{ Tag.Declaration, Tag.Declaration_, },
            //  2. <Declaration'> -> <Declaration> <Declaration'> 
            new Tag[]{ Tag.Declaration, Tag.Declaration_, },
            //  3. <Declaration'> -> 
            Array.Empty<Tag>(),
            //  4. <Declaration> -> <Solution>
            new Tag[]{ Tag.Solution, },
            //  5. <Declaration> -> <Path>
            new Tag[]{ Tag.SolutionPath, },
            //  6. <Declaration> -> <Presentation>
            new Tag[]{ Tag.Presentation, },
            //  7. <Declaration> -> <Application>
            new Tag[]{ Tag.Application, },
            //  8. <Declaration> -> <Infrastructure>
            new Tag[]{ Tag.Infrastructure, },
            //  9. <Declaration> -> <Contracts>
            new Tag[]{ Tag.Contracts, },
            // 10. <Declaration> -> <Domain>
            new Tag[]{ Tag.Domain, },
            // 11. <Declaration> -> <Test>
            new Tag[]{ Tag.Test, },
            // 12. <Declaration> -> <Packages>
            new Tag[]{ Tag.Packages, },
            // 13. <Solution> -> Solution = "string" @SolutionName
            new Tag[]{ Tag.SOLUTION, Tag.EQUAL, Tag.STRING, Tag._SolutionName, },
            // 14. <Path> -> Path = "string" @SolutionPath
            new Tag[]{ Tag.PATH, Tag.EQUAL, Tag.STRING, Tag._SolutionPath, },
            // 15. <Presentation> -> "Presentation" "=" "string" @PresentationName <OtherSources> @PresentationFiles 
            new Tag[]{ Tag.PRESENTATION, Tag.EQUAL, Tag.STRING, Tag._PresentationName, Tag.OtherSources, Tag._PresentationFiles, },
            // 16. <Application> -> "Application" "=" "string" @ApplicationName <OtherSources> @ApplicationFiles
            new Tag[]{ Tag.APPLICATION, Tag.EQUAL, Tag.STRING, Tag._ApplicationName, Tag.OtherSources, Tag._ApplicationFiles, },
            // 17. <Infrastructure> -> "Infrastructure" "=" "string" @InfrastructureName <OtherSources> @InfrastructureFiles
            new Tag[]{ Tag.INFRASTRUCTURE, Tag.EQUAL, Tag.STRING, Tag._InfrastructureName, Tag.OtherSources, Tag._InfrastructureFiles, },
            // // <Contracts> -> "Contracts" "=" "string" @ContractsName <OtherSources> @ContractsFiles <ListOfContractsByEntity> @ContractList
            // // <Contracts> -> "Contracts" "=" "string" @ContractsName <ListOfContractsByEntity> @ContractList <OtherSources> @ContractsFiles 
            // 18. <Contracts> -> "Contracts" "=" "string" @ContractsName <ListOfContractsByEntity> @ContractList
            new Tag[]{ Tag.CONTRACTS, Tag.EQUAL, Tag.STRING, Tag._ContractsName, Tag.ListOfContractsByEntity, Tag._ContractList, Tag.OtherSources, Tag._ContractsFiles, },
            //// <Domain> -> "Domain" "=" "string" @DomainName <OtherSources> @DomainFiles <ListOfObjects> @DomainList
            // 19. <Domain> -> "Domain" "=" "string" @DomainName <ListOfObjects> @DomainList <OtherSources> @DomainFiles 
            new Tag[]{ Tag.DOMAIN, Tag.EQUAL, Tag.STRING, Tag._DomainName, Tag.ListOfObjects, Tag._DomainList, Tag.OtherSources, Tag._DomainFiles, },
            // 20. <Test> -> "Test" "=" "string" @TestName <OtherSources> @TestFiles	
            new Tag[]{ Tag.TEST, Tag.EQUAL, Tag.STRING, Tag._TestName, Tag.OtherSources, Tag._TestFiles, },
            // 21. <Packages> -> "Use" <ListOfPackages> @Use
            new Tag[]{ Tag.USE, Tag.ListOfPackages, Tag._Use, },
            // 22. <ListOfContractsByEntity> -> "{" @CreateListOfContractsByEntity <ListOfContractsByEntity'> @Skip1 "}"
            new Tag[]{ Tag.LBRA, Tag._CreateListOfContractsByEntity, Tag.ListOfContractsByEntity_, Tag._Skip1, Tag.RBRA, },
            // 23. <ListOfContractsByEntity'> -> <ContractsByEntityDeclaration> @AddContractsByEntity <ListOfContractsByEntity'>
            new Tag[]{ Tag.ContractsByEntityDeclaration, Tag._AddContractsByEntity, Tag.ListOfContractsByEntity_, },
            // 24. <ListOfContractsByEntity'> -> @Echo
            new Tag[]{ Tag._Echo, },
            // 25. <ContractsByEntityDeclaration> -> "Entity" "=" "string" @EntityName <ListOfContracts> @EntityContracts
            new Tag[]{ Tag.ENTITY, Tag.EQUAL, Tag.STRING, Tag._EntityName, Tag.ListOfContracts, Tag._EntityContracts, },
            // 26. <ListOfContracts> -> "{" @CreateListOfContracts <ListOfContracts'> @Skip1 "}"
            new Tag[]{ Tag.LBRA, Tag._CreateListOfContracts, Tag.ListOfContracts_, Tag._Skip1, Tag.RBRA, },
            // 27. <ListOfContracts'> -> <ContractDeclaration> @AddContract <ListOfContracts'>
            new Tag[]{ Tag.ContractDeclaration, Tag._AddContract, Tag.ListOfContracts_, },
            // 28. <ListOfContracts'> -> @Echo
            new Tag[]{ Tag._Echo, },
            // 29. <ContractDeclaration> -> "Contract" "=" "string" @ContratName <ListOfProperties> @ContractProperties
            new Tag[]{ Tag.CONTRACT, Tag.EQUAL, Tag.STRING, Tag._ContratName, Tag.ListOfProperties, Tag._ContractProperties, },
            // 30. <ListOfObjects> -> "{" @CreateListOfObjects <ListOfObjects'> @Skip1 "}"
            new Tag[]{ Tag.LBRA, Tag._CreateListOfObjects, Tag.ListOfObjects_, Tag._Skip1, Tag.RBRA, },
            // 31. <ListOfObjects'> -> <Object> @AddObject <ListOfObjects'>
            new Tag[]{ Tag.Object, Tag._AddObject, Tag.ListOfObjects_, },
            // 32. <ListOfObjects'> -> @Echo
            new Tag[]{ Tag._Echo, },
            // 33. <Object> -> <Entity>
            new Tag[]{ Tag.Entity, },
            // 34. <Object> -> <Value>
            new Tag[]{ Tag.Value, },
            // 35. <Entity> -> "Entity" "=" "string" @EntityName <ListOfProperties> @DefineEntity
            new Tag[]{ Tag.ENTITY, Tag.EQUAL, Tag.STRING, Tag._EntityName, Tag.ListOfProperties, Tag._DefineEntity, },
            // 36. <Value> -> "Value" "=" "string" @ValueName <ListOfProperties> @DefineValue
            new Tag[]{ Tag.VALUE, Tag.EQUAL, Tag.STRING, Tag._ValueName, Tag.ListOfProperties, Tag._DefineValue, },
            // 37. <ListOfProperties> -> "[" @CreateListOfProperties <ListOfProperties'> @Skip1 "]"
            new Tag[]{ Tag.LCOL, Tag._CreateListOfProperties, Tag.ListOfProperties_, Tag._Skip1, Tag.RCOL, },
            // 38. <ListOfProperties'> -> <PropertyDeclaration> @AddProperty <ListOfProperties'>
            new Tag[]{ Tag.PropertyDeclaration, Tag._AddProperty, Tag.ListOfProperties_, },
            // 39. <ListOfProperties'> -> @Echo
            new Tag[]{ Tag._Echo, },
            // 40. <ListOfPackages> -> "{" @CreateListOfPackages <ListOfPackages'> @Skip1 "}"
            new Tag[]{ Tag.LBRA, Tag._CreateListOfPackages, Tag.ListOfPackages_, Tag._Skip1, Tag.RBRA, },
            // 41. <ListOfPackages'> -> "string" @AddPackage <ListOfPackages'>
            new Tag[]{ Tag.STRING, Tag._AddPackage, Tag.ListOfPackages_, },
            // 42. <ListOfPackages'> -> @Echo
            new Tag[]{ Tag._Echo, },
            // 43. <PropertyDeclaration> -> "Property" ":" <PropertyDeclarationBody>
            new Tag[]{ Tag.PROPERTY, Tag.SEMICOLON, Tag.PropertyDeclarationBody, },
            // 44. <PropertyDeclarationBody> -> "type" "=" "string" @Type "name" = "string" @PropertyDeclaration1
            new Tag[]{ Tag.TYPE, Tag.EQUAL, Tag.STRING, Tag._Type, Tag.NAME, Tag.EQUAL, Tag.STRING, Tag._PropertyDeclaration1, },
            // 45. <PropertyDeclarationBody> -> "name" = "string" @Name "type" "=" "string" @PropertyDeclaration2 
            new Tag[]{ Tag.NAME, Tag.EQUAL, Tag.STRING, Tag._Name, Tag.TYPE, Tag.EQUAL, Tag.STRING, Tag._PropertyDeclaration2, },
            // 46. <OtherSources> -> "{" @CreateListOfFiles <OtherSources'> @Skip1 "}"
            new Tag[]{ Tag.LBRA, Tag._CreateListOfFiles, Tag.OtherSources_, Tag._Skip1, Tag.RBRA, },
            // 47. <OtherSources> -> @CreateEmptyListOfFiles
            new Tag[]{ Tag._CreateEmptyListOfFiles, },
            // 48. <OtherSources'> -> "string" @AddOtherFile <OtherSources'>
            new Tag[]{ Tag.STRING, Tag._AddOtherFile, Tag.OtherSources_, },
            // 49. <OtherSources'> -> @Echo
            new Tag[]{ Tag._Echo, },
        };

        private static int[][] _M = new int[][] {
            // 0. <Start> -> <Text> @Done "#"
            //     FIRST = { Solution ... Use, # }
            //     FOLLOW = { }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] {  0,  0,  0,  0,  0,  0, -1, -1, -1, -1,  0, -1,  0,  0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, },
            // 1. <MainText> -> <Declaration> <Declaration'> 
            //     FIRST = { Solution ... Use, # }
            //     FOLLOW = { # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] {  1,  1,  1,  1,  1,  1, -1, -1, -1, -1,  1, -1,  1,  1, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 2. <Declaration'> -> <Declaration> <Declaration'> 
            //     FIRST = { Solution ... Use }
            // 3. <Declaration'> -> 
            //     FOLLOW = { # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] {  2,  2,  2,  2,  2,  2, -1, -1, -1, -1,  2, -1,  2,  2, -1, -1, -1, -1, -1, -1, -1, -1,  3, -1, },
            // 4. <Declaration> -> <Solution>
            //     FIRST = { Solution }
            // 5. <Declaration> -> <Path>
            //     FIRST = { Path }
            // 6. <Declaration> -> <Presentation>
            //     FIRST = { Presentation }
            // 7. <Declaration> -> <Application>
            //     FIRST = { Application }
            // 8. <Declaration> -> <Infrastructure>
            //     FIRST = { Infrastructure }
            // 9. <Declaration> -> <Contracts>
            //     FIRST = { Contracts }
            // 10. <Declaration> -> <Domain>
            //     FIRST = { Domain }
            // 11. <Declaration> -> <Test>
            //     FIRST = { Test }
            // 12. <Declaration> -> <Packages>
            //     FIRST = { Use }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] {  4,  5,  6,  7,  8,  9, -1, -1, -1, -1, 10, -1, 11, 12, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 13. <Solution> -> Solution = "string" @SolutionName
            //     FIRST = { Solution }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { 13, -2, -2, -2, -2, -2, -1, -1, -1, -1, -2, -1, -2, -2, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 14. <SolutionPath> -> Path = "string" @SolutionPath
            //     FIRST = { Path }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, 14, -2, -2, -2, -2, -1, -1, -1, -1, -2, -1, -2, -2, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 15. <Presentation> -> "Presentation" "=" "string" @PresentationName <OtherSources> @PresentationFiles 
            //     FIRST = { Presentation }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, -2, 15, -2, -2, -2, -1, -1, -1, -1, -2, -1, -2, -2, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 16. <Application> -> "Application" "=" "string" @ApplicationName <OtherSources> @ApplicationFiles
            //     FIRST = { Application }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, -2, -2, 16, -2, -2, -1, -1, -1, -1, -2, -1, -2, -2, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 17. <Infrastructure> -> "Infrastructure" "=" "string" @InfrastructureName <OtherSources> @InfrastructureFiles
            //     FIRST = { Infrastructure }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, -2, -2, -2, 17, -2, -1, -1, -1, -1, -2, -1, -2, -2, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            //// 18. <Contracts> -> "Contracts" ":" "string" @ContractsName <OtherSources> @ContractsFiles <ListOfContractsByEntity> @ContractList
            // 18. <Contracts> -> "Contracts" ":" "string" @ContractsName <ListOfContractsByEntity> @ContractList <OtherSources> @DomainFiles 
            //     FIRST = { Contracts }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, -2, -2, -2, -2, 18, -1, -1, -1, -1, -1, -1, -2, -2, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            // // 19. <Domain> -> "Domain" ":" "string" @DomainName <OtherSources> @DomainFiles <ListOfObjects> @DomainList
            // 19. <Domain> -> "Domain" ":" "string" @DomainName <ListOfObjects> @DomainList <OtherSources> @DomainFiles 
            //     FIRST = { Domain }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, -2, -2, -2, -2, -2, -1, -1, -1, -1, 19, -1, -2, -2, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 20. <Test> -> "Test" ":" "string" @TestName <OtherSources> @TestFiles	
            //     FIRST = { Test }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, -2, -2, -2, -2, -2, -1, -1, -1, -1, -2, -1, 20, -2, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 21. <Packages> -> "Use" <ListOfPackages> @Use
            //     FIRST = { Use }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, -2, -2, -2, -2, -2, -1, -1, -1, -1, -2, -1, -2, 21, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 22. <ListOfContractsByEntity> -> "{" @CreateListOfContractsByEntity <ListOfContractsByEntity'> @Skip1 "}"
            //     FIRST = { { }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, -2, -2, -2, -2, -2, -1, -1, -1, -1, -2, -1, -2, -2, -1, 22, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 23. <ListOfContractsByEntity'> -> <ContractsByEntityDeclaration> @AddContractsByEntity <ListOfContractsByEntity'>
            //     FIRST = { Entity }
            // 24. <ListOfContractsByEntity'> -> @Echo
            //     FOLLOW = { } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, 23, -1, -1, -1, -1, -1, -1, -1, -1, -1, 24, -1, -1, -1, -1, -1, -1, -1, },
            // 25. <ContractsByEntityDeclaration> -> "Entity" "=" "string" @EntityName <ListOfContracts> @EntityContracts
            //     FIRST = { Entity }
            //     FOLLOW = { Entity, } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, 25, -1, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, -1, -1, -1, -1, -1, -1, },
            // 26. <ListOfContracts> -> "{" @CreateListOfContracts <ListOfContracts'> @Skip1 "}"
            //     FIRST = { { }
            //     FOLLOW = { Entity, } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, -2, -1, -1, -1, -1, -1, -1, -1, -1, 26, -2, -1, -1, -1, -1, -1, -1, -1, },
            // 27. <ListOfContracts'> -> <ContractDeclaration> @AddContract <ListOfContracts'>
            //     FIRST = { Contract }
            // 28. <ListOfContracts'> -> @Echo
            //     FOLLOW = { } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, -1, 27, -1, -1, -1, -1, -1, -1, -1, -1, 28, -1, -1, -1, -1, -1, -1, -1, },
            // 29. <ContractDeclaration> -> "Contract" "=" "string" @ContratName <ListOfProperties> @ContractProperties
            //     FIRST = { Contract }
            //     FOLLOW = { Contract, } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, -1, 29, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, -1, -1, -1, -1, -1, -1, },
            // 30. <ListOfObjects> -> "{" @CreateListOfObjects <ListOfObjects'> @Skip1 "}"
            //     FIRST = { { }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, -2, -2, -2, -2, -2, -1, -1, -1, -1, -2, -1, -2, -2, -1, 30, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 31. <ListOfObjects'> -> <Object> @InsertObject <ListOfObjects'>
            //     FIRST = { Entity, Value }
            // 32. <ListOfObjects'> -> @Echo
            //     FOLLOW = { } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, 31, -1, -1, -1, -1, 31, -1, -1, -1, -1, 32, -1, -1, -1, -1, -1, -1, -1, },
            // 33. <Object> -> <Entity>
            //     FIRST = { Entity }
            // 34. <Object> -> <Value>
            //     FIRST = { Value }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            //     FOLLOW = { } }
            new int[] { -1, -1, -1, -1, -1, -1, 33, -1, -1, -1, -1, 34, -1, -1, -1, -1, -2, -1, -1, -1, -1, -1, -1, -1, },
            // 35. <Entity> -> "Entity" "=" "string" @EntityName <ListOfProperties> @DefineEntity
            //     FIRST = { Entity }
            //     FOLLOW = { Entity, Value, } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, 35, -1, -1, -1, -1, -2, -1, -1, -1, -1, -2, -1, -1, -1, -1, -1, -1, -1, },
            // 36. <Value> -> "Value" "=" "string" @ValueName <ListOfProperties> @DefineValue
            //     FIRST = { Value }
            //     FOLLOW = { Entity, Value, } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, -2, -1, -1, -1, -1, 36, -1, -1, -1, -1, -2, -1, -1, -1, -1, -1, -1, -1, },
            // 37. <ListOfProperties> -> "[" @CreateListOfProperties <ListOfProperties'> @Skip1 "]"
            //     FIRST = { [ }
            //     FOLLOW = { Entity, Value, } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, -2, -1, -1, -1, -1, -2, -1, -1, -1, -1, -2, 37, -1, -1, -1, -1, -1, -1, },
            // 38. <ListOfProperties'> -> <PropertyDeclaration> @AddProperty <ListOfProperties'>
            //     FIRST = { Property }
            // 39. <ListOfProperties'> -> @Echo
            //     FOLLOW = { ] }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 38, -1, -1, -1, 39, -1, -1, -1, -1, -1, },
            // 40. <ListOfPackages> -> "{" @CreateListOfPackages <ListOfPackages'> @Skip1 "}"
            //     FIRST = { { }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -2, -2, -2, -2, -2, -2, -1, -1, -1, -1, -2, -1, -2, -2, -1, 40, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 41. <ListOfPackages'> -> "string" @AddPackage <ListOfPackages'>
            //     FIRST = { string }
            // 42. <ListOfPackages'> -> @Echo
            //     FOLLOW = { } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 42, -1, -1, 41, -1, -1, -1, -1, },
            // 43. <PropertyDeclaration> -> "Property" ":" <PropertyDeclarationBody>
            //     FIRST = { Property }
            //     FOLLOW = { Property, ] }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 43, -1, -1, -1, -2, -1, -1, -1, -1, -1, },
            // 44. <PropertyDeclarationBody> -> "type" "=" "string" @Type "name" = "string" @PropertyDeclaration1
            //     FIRST = { Type }
            // 45. <PropertyDeclarationBody> -> "name" = "string" @Name "type" "=" "string" @PropertyDeclaration2 
            //     FIRST = { Name }
            //     FOLLOW = { Property, ] }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, 44, 45, -1, -1, -1, -1, -2, -1, -1, -1, -2, -1, -1, -1, -1, -1, },
            // 46. <OtherSources> -> "{" @CreateListOfFiles <OtherSources'> @Skip1 "}"
            //     FIRST = { { }
            // 47. <OtherSources> -> @CreateEmptyListOfFiles
            //     FIRST = { Solution ... Use, # }
            //     FOLLOW = { SOL PAT PRE APP INF CON DOM TST USE # }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { 47, 47, 47, 47, 47, 47, -1, -1, -1, -1, 47, -1, 47, 47, -1, 46, -1, -1, -1, -1, -1, -1, -2, -1, },
            // 48. <OtherSources'> -> "string" @AddOtherFile <OtherSources'>
            //     FIRST = { string }
            // 49. <OtherSources'> -> @Echo
            //     FOLLOW = { } }
            //         SOL PAT PRE APP INF CON ENT CNT TYP NME DOM VAL TST USE PRY   {   }   [   ] STR   =   :   #   e
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 49, -1, -1, 48, -1, -1, -1, -1, },
        };

        protected override AbstractTAG EndMark => Tag.ENDMARK;

        protected override AbstractTAG EmptyTag => Tag.EMPTY;

        public Parser()
            : base(_RHS, _M, new Scanner(), new Semantic(), new SolutionEnvironment())
        {
            // Nothing more todo
        }

        protected override void SaveAttributes(Stack<AbstractTAG> stk, AbstractTAG A, int p)
        {
            switch (p)
            {
                // 23. <ListOfContractsByEntity'> -> <ContractsByEntityDeclaration> @AddContractsByEntity <ListOfContractsByEntity'>
                case 23:
                    stk.ToArray()[1].SetAttribute(1, A.GetAttribute(0));
                    break;
                // 24. <ListOfContractsByEntity'> -> @Echo
                case 24:
                    stk.Peek().SetAttribute(0, A.GetAttribute(0));
                    break;
                // 27. <ListOfContracts'> -> <ContractDeclaration> @AddContract <ListOfContracts'>
                case 27:
                    stk.ToArray()[1].SetAttribute(1, A.GetAttribute(0));
                    break;
                // 28. <ListOfContracts'> -> @Echo
                case 28:
                    stk.Peek().SetAttribute(0, A.GetAttribute(0));
                    break;
                // 31. <ListOfObjects'> -> <Object> @InsertObject <ListOfObjects'>
                case 31:
                    stk.ToArray()[1].SetAttribute(1, A.GetAttribute(0));
                    break;
                // 32. <ListOfObjects'> -> @Echo
                case 32:
                    stk.Peek().SetAttribute(0, A.GetAttribute(0));
                    break;
                // 38. <ListOfProperties'> -> <PropertyDeclaration> @AddProperty <ListOfProperties'>
                case 38:
                    stk.ToArray()[1].SetAttribute(1, A.GetAttribute(0));
                    break;
                // 39. <ListOfProperties'> -> @Echo
                case 39:
                    stk.Peek().SetAttribute(0, A.GetAttribute(0));
                    break;
                // 41. <ListOfPackages'> -> "string" @AddPackage <ListOfPackages'>
                case 41:
                    stk.ToArray()[1].SetAttribute(0, A.GetAttribute(0));
                    break;
                // 42. <ListOfPackages'> -> @Echo
                case 42:
                    stk.Peek().SetAttribute(0, A.GetAttribute(0));
                    break;
                // 48. <OtherSources'> -> "string" @AddOtherFile <OtherSources'>
                case 48:
                    stk.ToArray()[1].SetAttribute(0, A.GetAttribute(0));
                    break;
                // 49. <OtherSources'> -> @Echo
                case 49:
                    stk.Peek().SetAttribute(0, A.GetAttribute(0));
                    break;
            }
        }
    }
}

