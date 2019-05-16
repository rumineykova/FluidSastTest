namespace FluidSastTest

module Adder = 
    
    open FluidTypes.Annotations
    open ScribbleGenerativeTypeProvider
    
    [<Refined("{v:int|v>=0}")>]
    type Nat = int
    
    let abs (x : int) : Nat =
        if x > 0 then x
        else -x
    
    type AdderC = 
       Provided.TypeProviderFile<"FSM/FsmAsstC.txt", "Adder", "C"
           ,"Config/configC.yaml", Delimiter=AdderData.delims
           ,TypeAliasing=AdderData.typeAliasing, AssertionsOn=true, ScribbleSource= ScribbleSource.File>
    
    
    let S = AdderC.S.instance
    let client = new AdderC()
    let c = client.Init().receiveHELLO(S).finish()

    (*
    let numIter = 1000

    let rec adderRec a b iter (c0:AdderC.State12) = 
       let res = new DomainModel.Buf<int>()
       //let c1= c0.receiveHELLO(S, res)
       let c = c0.sendHELLO(S, "hello")
       match iter with
           |0 -> 
               let c1 = c.sendBYE(S)
               let c2 = c1.receiveBYE(S)
               printfn "Fibo : %d" b
               let finalc = c2.finish()
               finalc
           |n -> 
               let c1 = c.sendADD(S, a)
               printfn "Send ADD"   
               let c2 = c1.receiveRES(S, res)
               (*let foo s = if s > 0 then true else false 
               let result = foo (res.getValue())*)
               adderRec b (res.getValue()) (n-1) c2

    let client = new AdderC()
    let res = new DomainModel.Buf<int>()
    let c = client.Init().receiveHELLO(S, res).sendHELLO(S, 2)
    let s1 = c |> adderRec 1 1 3
    
    *)