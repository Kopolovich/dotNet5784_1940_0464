﻿namespace BlApi;

public interface IEngineer
{
    /// <summary>
    /// adding new engineer to data 
    /// </summary>
    /// <param name="engineer"> logic engineer entity</param>
    public void Create(BO.Engineer? engineer);
    
    /// <summary>
    /// read engineer
    /// </summary>
    /// <param name="id"> id of requested engineer</param>
    /// <returns> logic engineer entity </returns>
    public BO.Engineer Read(int id);

    /// <summary>
    /// reads list of engineers
    /// </summary>
    /// <param name="filter">optional filter</param>
    /// <returns> collection of logic engineer entities </returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);

    /// <summary>
    /// reads collection of all inactive engineers
    /// </summary>
    /// <returns> collection of logic engineer entities </returns>
    public IEnumerable<BO.Engineer> ReadAllNotActive();

    /// <summary>
    /// updating engineer
    /// </summary>
    /// <param name="engineer"> updated engineer </param>
    public void Update(BO.Engineer? engineer);

    /// <summary>
    /// deleting engineer from data
    /// </summary>
    /// <param name="id"> id of requested engineer </param>
    public void Delete(int id);

    /// <summary>
    /// assigning task to engineer
    /// </summary>
    /// <param name="engineerId"> id of engineer </param>
    /// <param name="task"> requested task </param>
    public void AssignTaskToEngineer(int engineerId, BO.TaskInEngineer task);

    /// <summary>
    /// reactivates engineer
    /// </summary>
    /// <param name="engineer"> engineer to recover </param>
    public void RecoverEngineer(BO.Engineer engineer);
}
