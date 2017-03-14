#!/bin/sh

dotnet restore

dotnet ef database update

dotnet run
