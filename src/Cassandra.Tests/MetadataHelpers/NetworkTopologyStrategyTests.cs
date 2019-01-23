//
//      Copyright (C) 2012-2014 DataStax Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//

using System.Collections.Generic;

using Cassandra.MetadataHelpers;

using NUnit.Framework;

namespace Cassandra.Tests.MetadataHelpers
{
    [TestFixture]
    public class NetworkTopologyStrategyTests
    {
        [Test]
        public void TokenMap_IsDoneForToken_Should_Return_True_When_No_Host_In_Dc()
        {
            var ksReplicationFactor = new Dictionary<string, int>
            {
                {"dc1", 1},
                {"dc2", 3},
                {"dc3", 1}
            };
            var replicasByDc = new Dictionary<string, int>
            {
                {"dc1", 1},
                {"dc2", 3}
            };
            //no host in DC 3
            var datacenters = new Dictionary<string, TokenMap.DatacenterInfo>
            {
                {"dc1", new TokenMap.DatacenterInfo { HostLength = 10 } },
                {"dc2", new TokenMap.DatacenterInfo { HostLength = 10 } }
            };
            Assert.True(NetworkTopologyStrategy.IsDoneForToken(ksReplicationFactor, replicasByDc, datacenters));
        }

        [Test]
        public void TokenMap_IsDoneForToken_Should_Return_False_When_Not_Satisfied()
        {
            var ksReplicationFactor = new Dictionary<string, int>
            {
                {"dc1", 1},
                {"dc2", 3},
                {"dc3", 1}
            };
            var replicasByDc = new Dictionary<string, int>
            {
                {"dc1", 1},
                {"dc2", 1}
            };
            //no host in DC 3
            var datacenters = new Dictionary<string, TokenMap.DatacenterInfo>
            {
                {"dc1", new TokenMap.DatacenterInfo { HostLength = 10 } },
                {"dc2", new TokenMap.DatacenterInfo { HostLength = 10 } }
            };
            Assert.False(NetworkTopologyStrategy.IsDoneForToken(ksReplicationFactor, replicasByDc, datacenters));
        }
    }
}