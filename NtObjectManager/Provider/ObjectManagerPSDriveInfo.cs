﻿//  Copyright 2016 Google Inc. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using NtApiDotNet;
using System;
using System.Management.Automation;

namespace NtObjectManager.Provider
{
    internal class ObjectManagerPSDriveInfo : PSDriveInfo
    {
        public ObjectManagerPSDriveInfo(NtObject root, PSDriveInfo drive_info) 
            : base(drive_info)
        {
tq84.indent("[91mObjectManagerPSDriveInfo.ctor[0m");
            if (root is NtDirectory dir)
            {
tq84.print("root is NtDirectory, setting DirectoryRoot to NtDirectoryContainer");
                DirectoryRoot = new NtDirectoryContainer(dir);
            }
            else if (root is NtKey key)
            {
tq84.print("root is NtKey");
                bool open_for_backup = false;
                using (var token = NtToken.OpenProcessToken())
                {
                    if (token.SinglePrivilegeCheck(TokenPrivilegeValue.SeBackupPrivilege))
                    {
                        open_for_backup = true;
                    }
                }

tq84.print("Setting DirectoryRoot to NtKeyContainer");
                DirectoryRoot = new NtKeyContainer(key, open_for_backup);
            }
            else
            {
                throw new ArgumentException($"Invalid root object. {root.NtTypeName}");
            }
tq84.dedent("Leaving ObjectManagerPSDriveInfo.ctor()");
        }

        public NtObjectContainer DirectoryRoot { get; }

        public void Close()
        {
            DirectoryRoot?.Dispose();
        }
    }
}
