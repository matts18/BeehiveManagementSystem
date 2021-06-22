using System;
namespace BeehiveManagementSystem
{
    interface IWorker
    {
        string Job { get; }
        void WorkTheNextShift();
    }
}
