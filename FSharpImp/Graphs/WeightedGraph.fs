module FSharpImp.Graphs.WeightedGraph 

type Graph = (int * double) list array

let createGraph (vertexCount: int) : Graph = Array.create vertexCount []

let addEdge (x: int) (y: int) (weight: double) (g: Graph) = 
    g[x] <- (y, weight) :: g[x]
    g

let withEdge (a: char) (weight: double) (b: char) (g: Graph) =
    let x = (int)(a - 'A')
    let y = (int)(b - 'A')
    addEdge x y weight g

let addEdgeUn (x: int) (y: int) (weight: double) (g: Graph) = 
    g[x] <- (y, weight) :: g[x]
    g[y] <- (x, weight) :: g[y]
    g

let neighborsOf (x: int) (g: Graph) = g[x]

let len (g: Graph) = g.Length


   
 
        








