using Vuforia;

internal interface ITrackableEventHandler
{
    void OnTrackableStatusChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus);
}