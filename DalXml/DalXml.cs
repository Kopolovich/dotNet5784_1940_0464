﻿namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;
using System.Xml.Linq;

/// <summary>
/// Implements the IDal interface by initializing the sub-interfaces in the access classes
/// </summary>
sealed internal class DalXml : IDal
{
    public static IDal Instance => new DalXml();
    private DalXml() { }


    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public IUser User => new UserImplementation();

    public DateTime? StartDate { get => Config.ProjectStartDate; set => Config.ProjectStartDate = value; }

    public void Reset()
    {
        IEnumerable<DO.Task?> tasks = Task.ReadAll();
        if (tasks.Count() != 0)
        {
            foreach (var task in tasks)
                Task.Delete(task.Id);
        }

        IEnumerable<Dependency?> dependencies = Dependency.ReadAll();
        if (dependencies.Count() != 0)
        {
            foreach (var dependency in dependencies)
                Dependency.Delete(dependency.Id);
        }

        IEnumerable<Engineer?> engineers = Engineer.ReadAll();
        if (engineers.Count() != 0)
        {
            foreach (var engineer in engineers)
                Engineer.Delete(engineer.Id);
        }

        IEnumerable<User?> users = User.ReadAll(user => user.Role == UserRole.Engineer);
        if (users.Count() != 0)
        {
            foreach (var user in users)
                User.Delete(user.Id);
        }

        XMLTools.RemoveStartDate("data-config", "ProjectStartDate");
        XMLTools.ResetNextId("data-config", "NextTaskId");
        XMLTools.ResetNextId("data-config", "NextDependencyId");
    }

}
