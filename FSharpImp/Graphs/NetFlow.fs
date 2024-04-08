module FSharpImp.Graphs.NetFlow

type EdgeNode = {
    vertex: int
    capacity: int
    flow: int
    residual: int
}

