﻿using Microsoft.EntityFrameworkCore;

namespace NotePad.Models;

public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options) :base(options)
    {
            
    }
    public DbSet<ToDo> Todos => Set<ToDo>();
}