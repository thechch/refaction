#!/bin/sh

dotnet restore

dotnet ef migrations update

dotnet run