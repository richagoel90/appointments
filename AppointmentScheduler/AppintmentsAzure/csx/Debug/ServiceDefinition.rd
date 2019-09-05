<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AppintmentsAzure" generation="1" functional="0" release="0" Id="c475ec23-e4cd-453a-ba06-0c8d9e3f2ea9" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AppintmentsAzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="AppointmentScheduler:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/LB:AppointmentScheduler:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="AppointmentScheduler:DBConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/MapAppointmentScheduler:DBConnectionString" />
          </maps>
        </aCS>
        <aCS name="AppointmentScheduler:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/MapAppointmentScheduler:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="AppointmentSchedulerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/MapAppointmentSchedulerInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:AppointmentScheduler:Endpoint1">
          <toPorts>
            <inPortMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/AppointmentScheduler/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapAppointmentScheduler:DBConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/AppointmentScheduler/DBConnectionString" />
          </setting>
        </map>
        <map name="MapAppointmentScheduler:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/AppointmentScheduler/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapAppointmentSchedulerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/AppointmentSchedulerInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="AppointmentScheduler" generation="1" functional="0" release="0" software="D:\Richa Documents\GitHubRepos\appointments\AppointmentScheduler\AppintmentsAzure\csx\Debug\roles\AppointmentScheduler" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="DBConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AppointmentScheduler&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AppointmentScheduler&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/AppointmentSchedulerInstances" />
            <sCSPolicyUpdateDomainMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/AppointmentSchedulerUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/AppointmentSchedulerFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="AppointmentSchedulerUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="AppointmentSchedulerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="AppointmentSchedulerInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="fa8dc695-25a3-4ca8-81ba-6770bc4cc807" ref="Microsoft.RedDog.Contract\ServiceContract\AppintmentsAzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="f0b9b245-c982-41fd-b5ad-0d95ea6d6c46" ref="Microsoft.RedDog.Contract\Interface\AppointmentScheduler:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/AppintmentsAzure/AppintmentsAzureGroup/AppointmentScheduler:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>