﻿using BO;
namespace BlApi;

public interface IBl
{
    public ITask Task { get; }
    public IEngineer Engineer { get; }
    public ProjectStatus GetProjectStatus();
    public DateTime CreateProjectSchedule(DateTime projectStartDate);
    public void Reset();
    public void Initialize();

    #region Clock
    public DateTime Clock { get;}
    public void AddDay();
    public void AddWeek();
    public void AddMonth();
    public void AddYear();
    public void ResetClock();
    #endregion
}
