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
        timeineIdx = 0;  // �ε��� �ʱ�ȭ
    }
    private void Update()
    {
        CheckTimeline();
    }

    private void CheckTimeline()
    {
        // Ÿ�Ӷ��� ������ üũ�ߴ��� Ȯ��
        if (timeineIdx >= timeline.Length)
            return;

        // ���� �� �ð��� Ÿ�Ӷ��� �ð� ��
        if (Time.timeSinceLevelLoad >= timeline[timeineIdx].time)
        {
            // �� ����
            enemyGenerator.GetEnemy(timeline[timeineIdx].enemyType);

            // ���� Ÿ�Ӷ������� �̵�
            timeineIdx++;
        }
    }
}
