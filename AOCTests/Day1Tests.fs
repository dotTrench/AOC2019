module AOCTests.Day1Tests

open Xunit
open AOC2019.Day1

let assertEquals (expected: int) actual =
    Assert.Equal(expected, actual)

module Task1 =
    [<Fact>]
    let Example1() =
        task1 [ 12 ] |> assertEquals 2

    [<Fact>]
    let Example2() =
        task1 [ 14 ] |> assertEquals 2

    [<Fact>]
    let Example3() =
        task1 [ 1969 ] |> assertEquals 654

    [<Fact>]
    let Example4() =
        task1 [ 100756 ] |> assertEquals 33583

module Task2 =
    [<Fact>]
    let Example1() =
        task2 [ 14 ] |> assertEquals 2

    [<Fact>]
    let Example2() =
        task2 [ 1969 ] |> assertEquals 966

    [<Fact>]
    let Example3() =
        task2 [ 100756 ] |> assertEquals 50346
