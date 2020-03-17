﻿//-----------------------------------------------------------------------

// <copyright>
//
// Copyright (c) TU Chemnitz, Prof. Technische Thermodynamik
// Written by Noah Pflugradt.
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
// Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//  Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer
// in the documentation and/or other materials provided with the distribution.
//  All advertising materials mentioning features or use of this software must display the following acknowledgement:
//  “This product includes software developed by the TU Chemnitz, Prof. Technische Thermodynamik and its contributors.”
//  Neither the name of the University nor the names of its contributors may be used to endorse or promote products
//  derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE UNIVERSITY 'AS IS' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,
// BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE UNIVERSITY OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, S
// PECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; L
// OSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
// ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

// </copyright>

//-----------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using Automation;
using Common;
using Common.Tests;
using Database.Tables.Houses;
using NUnit.Framework;

namespace Database.Tests.Tables {
    [TestFixture]
    public class EnergyStorageTests : UnitTestBaseClass
    {
        [Test]
        [Category(UnitTestCategories.BasicTest)]
        public void LoadFromDatabaseTest() {
            var db = new DatabaseSetup(Utili.GetCurrentMethodAndClass(), DatabaseSetup.TestPackage.DatabaseIo);

            var storages = new ObservableCollection<EnergyStorage>();
            var loadTypes = db.LoadLoadTypes();
            EnergyStorage.LoadFromDatabase(storages, db.ConnectionString, loadTypes, false);
            // delete everything and check
            storages.Clear();
            db.ClearTable(EnergyStorage.TableName);
            db.ClearTable(EnergyStorageSignal.TableName);
            EnergyStorage.LoadFromDatabase(storages, db.ConnectionString, loadTypes, false);
            Assert.AreEqual(0, storages.Count);
            // add one and load again
            var stor = new EnergyStorage("tdlt", "desc", loadTypes[0], 10, 10, 0, 10, 0, 10,
                db.ConnectionString, Guid.NewGuid().ToString());
            stor.SaveToDB();
            stor.AddSignal(loadTypes[0], 100, 50, 60);
            EnergyStorage.LoadFromDatabase(storages, db.ConnectionString, loadTypes, false);
            Assert.AreEqual(1, storages.Count);
            Assert.AreEqual(1, storages[0].Signals.Count);
            // delete the loaded one
            storages[0].DeleteFromDB();
            storages.Clear();
            EnergyStorage.LoadFromDatabase(storages, db.ConnectionString, loadTypes, false);
            Assert.AreEqual(0, storages.Count);
            db.Cleanup();
        }
    }
}