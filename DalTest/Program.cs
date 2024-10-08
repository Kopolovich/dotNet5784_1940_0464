﻿using Dal;
using DalApi;
using DO;
using System.Collections.Specialized;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
namespace DalTest
{
   
    public class Program
    {
        //static readonly IDal s_dal = new DalList(); //stage 2
        //static readonly IDal s_dal = new DalXml(); //stage 3
        static readonly IDal s_dal = DalApi.Factory.Get; //stage 4

        static void Main(string[] args)
        {
            try
            {                          
                //Initialization.Do(s_dal); //for dalList - stage 2
                void mainMenu()
                {
                    Console.WriteLine("""
                        Choose entity:
                        Enter 0 to exit
                        Enter 1 for Task 
                        Enter 2 for Engineer
                        Enter 3 for Dependency
                        Enter 4 to initialize data
                        """);
                    int choice = int.Parse(Console.ReadLine());
                    while(choice != 0)
                    {
                        switch (choice)
                        {
                            case 0:
                                break; 
                            case 1:
                                { subMenuTask(); break; }
                            case 2:
                                { subMenuEngineer(); break; }
                            case 3:
                                { subMenuDependency(); break; }
                            case 4:
                                { initialize(); break; }    
                            default:
                                Console.WriteLine("please enter number between 0 and 3");
                                break;
                        }
                        Console.WriteLine("""
                        Choose entity:
                        Enter 0 to exit
                        Enter 1 for Task 
                        Enter 2 for Engineer
                        Enter 3 for Dependency
                        Enter 4 to initialize data
                        """);
                        choice = int.Parse(Console.ReadLine());

                    }

                }

                mainMenu();

                void subMenuTask()
                {
                    Console.WriteLine("""   
                        Choose method:
                        Enter 0 to exit
                        Enter 1 for create 
                        Enter 2 for read
                        Enter 3 for read all
                        Enter 4 for update
                        Enter 5 for delete
                        """);
                    int choice = int.Parse(Console.ReadLine());
                    while (choice != 0)
                    {
                        switch (choice)
                        {
                            case 0:
                                break;
                            case 1:
                                {
                                    DO.Task newTask = createNewTask(); //creating new task with info from user
                                    s_dal.Task.Create(newTask); //adding task to list
                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine("enter id of task:");
                                    int id;
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine($"{s_dal.Task.Read(id)}"); //Reading and printing
                                    break;
                                } 
                            case 3:
                                {
                                    List<DO.Task> tasks = (List<DO.Task>)s_dal.Task.ReadAll().ToList(); //reading list
                                    foreach (DO.Task task in tasks) 
                                        Console.WriteLine(task); //printing each task                            
                                    break;
                                }
                            
                            case 4:
                                {
                                    Console.WriteLine("enter id of task to update:");
                                    int id;
                                    int.TryParse(Console.ReadLine(), out id); 
                                    DO.Task currentTask = s_dal.Task.Read(id);
                                    if(currentTask == null)
                                        throw new DalDoesNotExistException($"Task with ID={id} does not exist");
                                    Console.WriteLine(currentTask); //printing current task
                                    DO.Task updatedTask = createUpdatedTask(currentTask);//creating new task with updated info
                                    s_dal.Task.Update(updatedTask);//updating
                                    break;
                                }

                            case 5:
                                {
                                    Console.WriteLine("enter id of task to delete:");
                                    int id;
                                    int.TryParse(Console.ReadLine(), out id);
                                    s_dal.Task.Delete(id); //delete
                                    break;
                                }
                            default:
                                Console.WriteLine("please enter number between 0 and 5");
                                break;
                        }
                        Console.WriteLine("""   
                        Choose method:
                        Enter 0 to exit
                        Enter 1 for create 
                        Enter 2 for read
                        Enter 3 for read all
                        Enter 4 for update
                        Enter 5 for delete
                        """);
                        choice = int.Parse(Console.ReadLine());

                    }
                }
                void subMenuEngineer()
                {
                    Console.WriteLine("""   
                        Choose method:
                        Enter 0 to exit
                        Enter 1 for create 
                        Enter 2 for read
                        Enter 3 for read all
                        Enter 4 for update
                        Enter 5 for delete
                        """);
                    int choice = int.Parse(Console.ReadLine());
                    while (choice != 0)
                    {
                        switch (choice)
                        {
                            case 0:
                                break;
                            case 1:
                                {
                                    Engineer newEngineer = createNewEngineer(); //creating new engineer with info from user
                                    s_dal.Engineer.Create(newEngineer); //adding to list
                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine("enter id of Engineer:");
                                    int id;
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine($"{s_dal.Engineer.Read(id)}"); //reading and printing
                                    break;
                                }
                            case 3:
                                {
                                    List<Engineer> engineers = (List<Engineer>)s_dal.Engineer.ReadAll().ToList(); //reading list
                                    foreach (Engineer engineer in engineers)
                                        Console.WriteLine(engineer); //print each engineer
                                    break;
                                }

                            case 4:
                                {
                                    Console.WriteLine("enter id of engineer to update:");
                                    int id;
                                    int.TryParse(Console.ReadLine(), out id);
                                    Engineer currentEngineer = s_dal.Engineer.Read(id);
                                    if (currentEngineer == null)
                                        throw new DalDoesNotExistException($"Engineer with ID={id} does not exist");
                                    Console.WriteLine(currentEngineer); //printing current engineer                                
                                    Engineer updatedEngineer = createUpdatedEngineer(currentEngineer);
                                    s_dal.Engineer.Update(updatedEngineer); //updating
                                    break;
                                }

                            case 5:
                                {
                                    Console.WriteLine("enter id of engineer to delete:");
                                    int id;
                                    int.TryParse(Console.ReadLine(), out id);
                                    s_dal.Engineer.Delete(id); //delete
                                    break;
                                }
                            default:
                                Console.WriteLine("please enter number between 0 and 5");
                                break;
                        }
                        Console.WriteLine("""   
                        Choose method:
                        Enter 0 to exit
                        Enter 1 for create 
                        Enter 2 for read
                        Enter 3 for read all
                        Enter 4 for update
                        Enter 5 for delete
                        """);
                        choice = int.Parse(Console.ReadLine());

                    }
                }
                void subMenuDependency()
                {
                    Console.WriteLine("""   
                        Choose method:
                        Enter 0 to exit
                        Enter 1 for create 
                        Enter 2 for read
                        Enter 3 for read all
                        Enter 4 for update
                        Enter 5 for delete
                        """);
                    int choice = int.Parse(Console.ReadLine());
                    while (choice != 0)
                    {
                        switch (choice)
                        {
                            case 0:
                                break;
                            case 1:
                                {
                                    Dependency newDependency = createNewDependency(); //creating new dependency
                                    s_dal.Dependency.Create(newDependency); //adding to list
                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine("enter id of Dependency:");
                                    int id;
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine($"{s_dal.Dependency.Read(id)}"); //reading and printing
                                    break;
                                }
                            case 3:
                                {
                                    List<Dependency> dependencies = (List<Dependency>)s_dal.Dependency.ReadAll().ToList(); //reading list
                                    foreach (Dependency dependency in dependencies)
                                        Console.WriteLine(dependency); //print each dependency
                                    break;
                                }

                            case 4:
                                {
                                    Console.WriteLine("enter id of Dependency to update:");
                                    int id;
                                    int.TryParse(Console.ReadLine(), out id);
                                    Dependency currentDependency = s_dal.Dependency.Read(id);
                                    if (currentDependency == null)
                                        throw new DalDoesNotExistException($"Dependency with ID={id} does not exist");
                                    Console.WriteLine(currentDependency); //printing current dependency
                                    Dependency updatedDependency = createUpdatedDependecy(currentDependency); 
                                    s_dal.Dependency.Update(updatedDependency); //update
                                    break;
                                }

                            case 5:
                                {
                                    Console.WriteLine("enter id of Dependency to delete:");
                                    int id;
                                    int.TryParse(Console.ReadLine(), out id);
                                    s_dal.Dependency.Delete(id); //delete
                                    break;
                                }
                            default:
                                Console.WriteLine("please enter number between 0 and 5");
                                break;
                        }
                        Console.WriteLine("""   
                        Choose method:
                        Enter 0 to exit
                        Enter 1 for create 
                        Enter 2 for read
                        Enter 3 for read all
                        Enter 4 for update
                        Enter 5 for delete
                        """);
                        choice = int.Parse(Console.ReadLine());

                    }
                }
                void initialize()
                {
                    Console.Write("Would you like to create Initial data? (Y/N)"); 
                    string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); 
                    if (ans == "Y")
                    {
                        s_dal.Reset(); //emptying files
                        Initialization.Do(); //initializing files
                    }
                       
                }

                //methods to create new items with details recieves from user
                DO.Task createNewTask()
                {
                    Console.WriteLine("enter number between 0 to 4 for level of complexity (Beginner, AdvancedBeginner, Intermediate, Advanced, Expert)");
                    int levelInt;
                    int.TryParse(Console.ReadLine(), out levelInt);
                    EngineerExperience level = (EngineerExperience)levelInt;

                    Console.WriteLine("enter alias:");
                    string? alias = Console.ReadLine();

                    Console.WriteLine("enter description:");
                    string? description = Console.ReadLine();

                    Console.WriteLine("Enter Required Effort Time:");
                    TimeSpan? requiredEffortTime;
                    TimeSpan time;
                    requiredEffortTime = TimeSpan.TryParse(Console.ReadLine(), out time) ? time : null;

                    Console.WriteLine("enter true if task is milestone, otherwise false");
                    bool? isMilestone = bool.Parse(Console.ReadLine());
                    
                    Console.WriteLine("enter start date:");
                    DateTime? startDate;
                    DateTime date;
                    startDate = DateTime.TryParse(Console.ReadLine(), out date) ? date : null;

                    Console.WriteLine("enter scheduled date:");
                    DateTime? scheduledDate;
                    scheduledDate = DateTime.TryParse(Console.ReadLine(), out date) ? date : null;

                    Console.WriteLine("enter complete date");
                    DateTime? completeDate;
                    completeDate = DateTime.TryParse(Console.ReadLine(), out date) ? date : null;

                    Console.WriteLine("enter deliverables:");
                    string? deliverables = Console.ReadLine();

                    Console.WriteLine("enter remarks: (optional)");
                    string? remarks = Console.ReadLine();

                    Console.WriteLine("enter id of engineer working on task:");
                    int? engineerId;
                    int engId;
                    engineerId = int.TryParse(Console.ReadLine(), out engId) ? engId : null;

                    DO.Task? task = new(0, level, DateTime.Now, true, description, alias, requiredEffortTime,
                        isMilestone, startDate, scheduledDate, null, completeDate, deliverables, remarks, engineerId);
                    return task;
                }
                Engineer createNewEngineer() 
                {
                    Console.WriteLine("enter id of engineer:");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);

                    Console.WriteLine("enter level of engineer:");
                    int levelInt;
                    int.TryParse(Console.ReadLine(), out levelInt);
                    EngineerExperience level = (EngineerExperience)levelInt;

                    Console.WriteLine("enter an email:");
                    string? email = Console.ReadLine();

                    Console.WriteLine("enter hourly salary:");
                    double cost;                    
                    double.TryParse(Console.ReadLine(), out cost);

                    Console.WriteLine("enter a name:");
                    string? name = Console.ReadLine();

                    Engineer engineer = new(id, level, true, email, cost, name);
                    return engineer; 
                }
                Dependency createNewDependency()
                {
                    Console.WriteLine("enter id of dependent task:");
                    int dependentTaskId;
                    int.TryParse(Console.ReadLine(), out dependentTaskId);

                    Console.WriteLine("enter id of task that is dependent on:");
                    int dependsOnTaskId;
                    int.TryParse(Console.ReadLine(), out dependsOnTaskId);

                    Dependency dependency = new(0, dependentTaskId,dependsOnTaskId);
                    return dependency;
                }
                //methods to create new items with updated details from user
                DO.Task createUpdatedTask(DO.Task oldTask)
                {
                    Console.WriteLine("enter new values only for the fields you want to update, the rest will stay without change");
                    
                    Console.WriteLine("enter number between 0 to 4 for level of complexity (Beginner, AdvancedBeginner, Intermediate, Advanced, Expert)");
                    int levelInt;
                    bool success = int.TryParse(Console.ReadLine(), out levelInt);
                    EngineerExperience level = success ? (EngineerExperience)levelInt : oldTask.Complexity;

                    Console.WriteLine("enter alias:");
                    string? alias = Console.ReadLine();
                    if (alias == "") alias = oldTask.Alias;

                    Console.WriteLine("enter description:");
                    string? description = Console.ReadLine();
                    if (description == "") description = oldTask.Description;

                    Console.WriteLine("Enter Required Effort Time:");
                    TimeSpan? requiredEffortTime;
                    TimeSpan time;
                    requiredEffortTime = TimeSpan.TryParse(Console.ReadLine(), out time) ? time : oldTask.RequiredEffortTime;
                    
                    Console.WriteLine("enter true if task is milestone, otherwise false");
                    bool? isMilestone;
                    bool isMile;
                    isMilestone = bool.TryParse(Console.ReadLine(), out isMile) ? isMile : oldTask.IsMilestone;
                                  
                    Console.WriteLine("enter start date:");
                    DateTime? startDate;
                    DateTime date;
                    startDate = DateTime.TryParse(Console.ReadLine(), out date) ? date : oldTask.StartDate;


                    Console.WriteLine("enter scheduled date:");
                    DateTime? scheduledDate;
                    scheduledDate = DateTime.TryParse(Console.ReadLine(), out date) ? date:oldTask.ScheduledDate;

                    Console.WriteLine("enter complete date");
                    DateTime? completeDate;
                    completeDate = DateTime.TryParse(Console.ReadLine(), out date) ? date : oldTask.CompleteDate;

                  
                    Console.WriteLine("enter deliverables:");
                    string? deliverables = Console.ReadLine();
                    if(deliverables == "") deliverables = oldTask.Deliverables;

                    Console.WriteLine("enter remarks: (optional)");
                    string? remarks = Console.ReadLine();
                    if(remarks == "") remarks = oldTask.Remarks;

                    Console.WriteLine("enter id of engineer working on task:");
                    int? engineerId;
                    int engId;
                    engineerId = int.TryParse(Console.ReadLine(), out engId) ? engId : oldTask.EngineerId;

                    DO.Task? task = new(oldTask.Id, level, oldTask.CreatedAtDate, true, description, alias, requiredEffortTime,
                        isMilestone, startDate, scheduledDate, null, completeDate, deliverables, remarks, engineerId);
                    return task;
                }
                Engineer createUpdatedEngineer(Engineer oldEngineer)
                {
                    Console.WriteLine("enter new values only for the fields you want to update, the rest will stay without change");

                    Console.WriteLine("enter number between 0 to 4 for level of complexity (Beginner, AdvancedBeginner, Intermediate, Advanced, Expert)");
                    int levelInt;
                    bool success = int.TryParse(Console.ReadLine(), out levelInt);
                    EngineerExperience level = success ? (EngineerExperience)levelInt : oldEngineer.Level;

                    Console.WriteLine("enter an email:");
                    
                    string? email = Console.ReadLine();
                    if(email == "") email = oldEngineer.Email;

                    Console.WriteLine("enter hourly salary:");
                    double? cost;
                    double _cost;
                    cost = double.TryParse(Console.ReadLine(), out _cost) ? _cost : oldEngineer.Cost;

                    Console.WriteLine("enter a name:");
                    string? name = Console.ReadLine();
                    if(name == "") name = oldEngineer.Name;

                    Engineer engineer = new(oldEngineer.Id, level, true, email, cost, name); //creating new engineer with updated details
                    return engineer;
                }
                Dependency createUpdatedDependecy(Dependency oldDependency)
                {
                    Console.WriteLine("enter new values only for the fields you want to update, the rest will stay without change");

                    Console.WriteLine("enter id of dependent task:");
                    int dependentTaskId;
                    dependentTaskId = int.TryParse(Console.ReadLine(), out dependentTaskId) ? dependentTaskId : oldDependency.DependentTask;

                    Console.WriteLine("enter id of task that is dependent on:");
                    int dependsOnTaskId;
                    dependsOnTaskId  = int.TryParse(Console.ReadLine(), out dependsOnTaskId) ? dependsOnTaskId : oldDependency.DependsOnTask;
                    
                    Dependency dependency = new(oldDependency.Id, dependentTaskId, dependsOnTaskId);
                    return dependency;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}