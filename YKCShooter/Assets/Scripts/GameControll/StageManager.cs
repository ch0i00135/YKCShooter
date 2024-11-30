using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public StageData stageData;
    public EnemyGenerator enemyGenerator;
    StageTimeline[] timeline;
    int timeineIdx;

    private void Start()
    {
        timeline = stageData.timeline;
        timeineIdx = 0;  // 인덱스 초기화
    }
    private void Update()
    {
        CheckTimeline();
    }

    private void CheckTimeline()
    {
        // 타임라인 끝까지 체크했는지 확인
        if (timeineIdx >= timeline.Length)
            return;

        // 현재 씬 시간과 타임라인 시간 비교
        if (Time.timeSinceLevelLoad >= timeline[timeineIdx].time)
        {
            // 적 생성
            enemyGenerator.GetEnemy(timeline[timeineIdx].enemyType);

            // 다음 타임라인으로 이동
            timeineIdx++;
        }
    }
}
