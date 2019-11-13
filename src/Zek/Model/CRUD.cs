using System;

namespace Zek.Model
{
    [Flags]
    public enum CRUD
    {
        Create = 1,
        Read = 2,
        Update = 4,
        Delete = 8
    }


    [Flags]
    public enum CRUD2
    {
        Create = CRUD.Create,
        Read = CRUD.Read,
        Update = CRUD.Update,
        Delete = CRUD.Delete,
        Approve = 16,
        Disapprove = 32,
        Block = 64,
        Unblock = 128,
        Import = 256,
        Export = 512,
        Copy = 1024,
    }
}
