using System.IO;
using UnityEditor;
using UnityEngine;
using Improbable.Gdk.Core;

namespace Fps.Editor
{
    public class SnapshotMenu : MonoBehaviour
    {
        public static readonly string DefaultSnapshotPath =
            Path.Combine(Application.dataPath, "../../../snapshots/default.snapshot");

        public static readonly string CloudSnapshotPath =
            Path.Combine(Application.dataPath, "../../../snapshots/cloud.snapshot");

        private static void AddCommonEntities(Snapshot snapshot)
        {
            snapshot.AddEntity(Common.FpsEntitiesTemplates.PlayerSpawner());
        }

        [MenuItem("SpatialOS/Generate Fps Template")]
        private static void GenerateFpsSnapshot()
        {
            var localSnapshot = new Snapshot();
            var cloudSnapshot = new Snapshot();

            AddCommonEntities(localSnapshot);
            AddCommonEntities(cloudSnapshot);

            SaveSnapshot(DefaultSnapshotPath, localSnapshot);
            SaveSnapshot(CloudSnapshotPath, cloudSnapshot);
        }

        private static void SaveSnapshot(string path, Snapshot snapshot)
        {
            snapshot.WriteToFile(path);
            Debug.LogFormat("Successfully generated initial world snapshot at {0}", path);
        }
    }
}