module AOCTests.Day2Tests

open Xunit
open AOC2019.Day2

let assertEquals (expected: 'T) (actual: 'T) =
    Assert.Equal<'T>(expected, actual)

module Task1 =
    [<Fact>]
    let Example1() =
        executeTape [ 1; 0; 0; 0; 99 ] |> assertEquals [ 2; 0; 0; 0; 99 ]

    [<Fact>]
    let Example2() =
        executeTape [ 2; 3; 0; 3; 99 ] |> assertEquals [ 2; 3; 0; 6; 99 ]
    
    [<Fact>]
    let Example3() =
        executeTape [ 2; 4; 4; 5; 99; 0 ] |> assertEquals [ 2; 4; 4; 5; 99; 9801 ]
    
    [<Fact>]
    let Example4() =
        executeTape [ 1; 1; 1; 4; 99; 5; 6; 0; 99 ] |> assertEquals [ 30; 1; 1; 4; 2; 5; 6; 0; 99 ]
