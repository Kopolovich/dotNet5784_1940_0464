﻿namespace Dal;
using DalApi;
using DO;

/// <summary>
/// Implementation of CRUD methods for Task entity
/// </summary>
public class TaskImplementation : ITask
{
    /// <summary>
    /// adding new Task to list
    /// </summary>
    /// <param name="item">refernce to new item to add</param>
    /// <returns>Id of new Task</returns>
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task newTask = item with { Id = newId };
        DataSource.Tasks.Add(newTask);
        return newId;
    }

    /// <summary>
    /// deletes requested Task from list
    /// </summary>
    /// <param name="id">id of Task to delete</param>
    /// <exception cref="Exception">if requested Task not found </exception>
    public void Delete(int id)
    {
        Task? found = DataSource.Tasks.Find(x => x.Id == id);
        if (found == null)
            throw new Exception($"Task with ID={id} does Not exist");
        else
            DataSource.Tasks.Remove(found);
    }

    /// <summary>
    /// retrievs requested Task
    /// </summary>
    /// <param name="id">id of Task to retrieve</param>
    /// <returns>retrieved Task</returns>
    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(x => x.Id == id);
    }

    /// <summary>
    /// retreives list of Tasks
    /// </summary>
    /// <returns>copy of list of Tasks</returns>
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    /// <summary>
    /// updates existing Task
    /// </summary>
    /// <param name="item">updated Task</param>
    /// <exception cref="Exception">if requested Task not found </exception>
    public void Update(Task item)
    {
        Task? found = DataSource.Tasks.Find(x => x.Id == item.Id);
        if (found == null)
            throw new Exception($"Task with ID={item.Id} does Not exist");
        else
        {
            DataSource.Tasks.Remove(found);
            DataSource.Tasks.Add(item);
        }
    }

    /// <summary>
    /// help method to find id of task by description
    /// </summary>
    /// <param name="description">description of task to search for</param>
    /// <returns>id of requested task</returns>
    public int? FindId(string description)
    {
        Task? task = DataSource.Tasks.Find(x => x.Description == description);
        if (task == null)
            return null;
        return task.Id;
    }
}



