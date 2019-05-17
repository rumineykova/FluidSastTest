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
       Provided.STP<"FSM\FSMAsstC.txt", "Adder", "C"
           ,"Config\configC.yaml", Delimiter=AdderData.delims
           ,TypeAliasing=AdderData.typeAliasing, AssertionsOn=true, ScribbleSource= ScribbleSource.File>
    
    let hello (x:int) =  
        printf "Received %i" x
    
    let S = AdderC.S.instance
    let client = new AdderC()
    let c = client.Init()//receiveHELLO(S).finish()
    let s1 = c.receiveHELLO(S, hello).finish()