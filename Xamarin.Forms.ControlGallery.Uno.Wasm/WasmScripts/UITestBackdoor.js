class UITestBackdoor {

    static Reset() {
        if (!UITestBackdoor.reset) {
            UITestBackdoor.reset = Module.mono_bind_static_method("[Xamarin.Forms.ControlGallery.Uno.Wasm] Xamarin.Forms.ControlGallery.WindowsUniversal.App:Reset");
        }

        return UITestBackdoor.reset();
    }
}