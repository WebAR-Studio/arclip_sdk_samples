mergeInto(LibraryManager.library, {
        ARLib_Initialize: function() {
            window.ARLib.Initialize()
        },

        ARLib_EnableCamera: function() {
            window.ARLib.camera.EnableCamera()
        },

        ARLib_DisableCamera: function() {
            window.ARLib.camera.DisableCamera()
        },

        ARLib_DisableTracking: function() {
            window.ARLib.tracking.DisableTracking()
        },

        ARLib_EnableSurfaceTracking: function(axis) {
            window.ARLib.tracking.EnableSurfaceTracking(UTF8ToString(axis))
        },

        ARLib_AddTrackingImage: function(data) {
            window.ARLib.tracking.AddTrackingImage(UTF8ToString(data))
        },

        ARLib_RemoveTrackingImage: function(name) {
            window.ARLib.tracking.RemoveTrackingImage(UTF8ToString(name))
        },

        ARLib_RemoveAllTrackingImages: function() {
            window.ARLib.tracking.RemoveAllTrackingImages()
        },

        ARLib_EnableImageTracking: function() {
            window.ARLib.tracking.EnableImageTracking()
        },

        ARLib_SetupVPS: function(data) {
            window.ARLib.vps.SetupVPS(UTF8ToString(data))
        },

        ARLib_StartVPS: function() {
            window.ARLib.vps.StartVPS()
        },

        ARLib_StopVPS: function() {
            window.ARLib.vps.StopVPS()
        },

        ARLib_PauseVPS: function() {
            window.ARLib.vps.PauseVPS()
        },

        ARLib_ResetTracking: function() {
            window.ARLib.vps.ResetTracking()
        },

        ARLib_ResumeVPS: function() {
            window.ARLib.vps.ResumeVPS()
        },

        ARLib_SetLocationIds: function() {
            window.ARLib.vps.SetLocationIds()
        },

        ARLib_SetAnimationTime: function(value) {
            window.ARLib.vps.SetAnimationTime(value)
        },

        ARLib_SetSendFastPhotoDelay: function(value) {
            window.ARLib.vps.SetSendFastPhotoDelay(value)
        },

        ARLib_SetSendPhotoDelay: function(value) {
            window.ARLib.vps.SetSendPhotoDelay(value)
        },

        ARLib_SetDistanceForInterp: function(value) {
            window.ARLib.vps.SetDistanceForInterp(value)
        },

        ARLib_SetGpsAccuracyBarrier: function(value) {
            window.ARLib.vps.SetGpsAccuracyBarrier(value)
        },

        ARLib_SetTimeOutDuration: function(value) {
            window.ARLib.vps.SetTimeOutDuration(value)
        },

        ARLib_SetFirstRequestDelay: function(value) {
            window.ARLib.vps.SetFirstRequestDelay(value)
        },

        ARLib_SetAngleForInterp: function(value) {
            window.ARLib.vps.SetAngleForInterp(value)
        },
});