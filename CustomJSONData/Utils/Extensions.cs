﻿using CustomJSONData.CustomBeatmap;
using CustomJSONData.CustomLevelInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomJSONData.Utils
{
    public static class Extensions
    {
        public static dynamic getCustomData(this IDifficultyBeatmap difficultyBeatmap)
        {
            if (difficultyBeatmap is CustomDifficultyBeatmap cd)
                if (cd.beatmapData is CustomBeatmapData cb)
                    return cb.customData;
            return null;
        }
        public static dynamic getLevelCustomData(this IDifficultyBeatmap difficultyBeatmap)
        {
            if (difficultyBeatmap is CustomDifficultyBeatmap cd)
                if (cd.beatmapData is CustomBeatmapData cb)
                    return cb.levelCustomData;
            return null;
        }
        public static dynamic getLevelCustomData(this IPreviewBeatmapLevel previewBeatmapLevel)
        {
            if (previewBeatmapLevel is CustomPreviewBeatmapLevel cpbl)
                if (cpbl.standardLevelInfoSaveData is CustomLevelInfoSaveData clisd)
                    return clisd.customData;
            return null;
        }
    }
}