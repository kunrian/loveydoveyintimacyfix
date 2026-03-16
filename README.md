Assumed problem

During lovin animations, Intimacy: a lovin expansion appears to call SetAllGraphicsDirty() every tick in its lovin job drivers. That would force constant pawn graphic rebuilds. 
With THIGAPPE / APPUtilities installed, those rebuilds repeatedly hit APP’s expensive WornGraphicPath fallback logic, which appears to be the main reason TPS drops so sharply during lovin.


Assumed fix

A likely compatibility fix would be to change StartLovinActions() in:

 - JobDriver_Sex
 - JobDriver_Sex_Mechanitor
so that it only sets IsCurrentlyLovin = true and calls SetAllGraphicsDirty() once when lovin begins, instead of doing that every tick for the full duration of the animation.


Why that would help

The apparel-hiding logic already appears to key off the live current lovin job driver and its IsCurrentlyLovin state, so one initial graphics refresh should be enough to enter the lovin visual state.
Repeating a full graphics invalidation every tick seems unnecessary and likely creates the redraw storm that drags APPUtilities into the hot path over and over.
Reducing it to a one-time invalidation should keep the intended visual behavior while removing the main performance hit.
