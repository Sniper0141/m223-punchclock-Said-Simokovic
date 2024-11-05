using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet;

public static class DbInitializer
{
    public static void Initialize(DatabaseContext context)
    {
        context.Database.ExecuteSql($"DROP TABLE IF EXISTS \"Category\"");
        context.Database.ExecuteSql($"""
                                      CREATE TABLE public."Category"(
                                         "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ), 
                                         "Title" VARCHAR(255) NOT NULL,
                                         CONSTRAINT "PK_Category" PRIMARY KEY ("Id"))
                                     """);
        
        context.Database.ExecuteSql($"DROP TABLE IF EXISTS \"Entry\"");
        context.Database.ExecuteSql($"""
                                     CREATE TABLE public."Entry"(
                                        "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ), 
                                        "CheckIn" timestamp with time zone NOT NULL,
                                        "CheckOut" timestamp with time zone NOT NULL,
                                        CONSTRAINT "PK_Entry" PRIMARY KEY ("Id"),
                                        CONSTRAINT "FK_Category" FOREIGN KEY ("Category.Id"))
                                    """);
        
        context.Database.ExecuteSql($"DROP TABLE IF EXISTS \"Tag\"");
        context.Database.ExecuteSql($"""
                                      CREATE TABLE public."Tag"(
                                         "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ), 
                                         "Title" VARCHAR(255) NOT NULL,
                                         CONSTRAINT "PK_Tag" PRIMARY KEY ("Id"))
                                     """);
        
        // In-between-table
        context.Database.ExecuteSql($"DROP TABLE IF EXISTS \"EntryTag\"");
        context.Database.ExecuteSql($"""
                                      CREATE TABLE public."EntryTag"(
                                         CONSTRAINT "FK_Entry" FOREIGN KEY ("Entry.Id"),
                                         CONSTRAINT "FK_Tag" FOREIGN KEY ("Tag.Id"))
                                     """);
    }
}