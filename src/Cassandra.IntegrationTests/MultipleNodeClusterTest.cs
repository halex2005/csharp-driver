﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Cassandra.IntegrationTests
{
    /// <summary>
    /// Represents a set of tests that reuse an test cluster of n node
    /// </summary>
    [TestFixture]
    public class MultipleNodesClusterTest
    {
        /// <summary>
        /// Gets or sets the Cluster builder for this test
        /// </summary>
        protected virtual Builder Builder { get; set; }

        protected virtual CcmClusterInfo CcmClusterInfo { get; set; }

        protected virtual Cluster Cluster { get; set; }

        protected virtual int NodeLength { get; set; }

        protected virtual ISession Session { get; set; }

        /// <summary>
        /// Creates a new instance of MultipleNodeCluster Test
        /// </summary>
        /// <param name="nodeLength">Determines the amount of nodes in the test cluster</param>
        public MultipleNodesClusterTest(int nodeLength)
        {
            this.NodeLength = nodeLength;
        }

        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            this.CcmClusterInfo = TestUtils.CcmSetup(NodeLength, Builder, "tester");
            this.Cluster = this.CcmClusterInfo.Cluster;
            this.Session = this.CcmClusterInfo.Session;
        }

        [TestFixtureTearDown]
        public virtual void TestFixtureTearDown()
        {
            try
            {
                //Try to close the connections
                Session.Dispose();
            }
            catch
            {

            }
            TestUtils.CcmRemove(this.CcmClusterInfo);
        }
    }
}