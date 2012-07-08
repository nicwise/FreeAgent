using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;


namespace FreeAgent
{
	public class ProjectClient : ResourceClient<ProjectWrapper, ProjectsWrapper, Project>
	{
		public ProjectClient(FreeAgentClient client) : base(client) {}
		

        public override string ResouceName { get { return "projects"; } } 

        public override ProjectWrapper WrapperFromSingle(Project single)
        {
            return new ProjectWrapper { project = single };
        }
        public override List<Project> ListFromWrapper(ProjectsWrapper wrapper)
        {
            return wrapper.projects;
        }

        public override Project SingleFromWrapper(ProjectWrapper wrapper)
        {
            return wrapper.project;
        }
     
        public override void CustomizeAllRequest(RestRequest request)
        {
            request.AddParameter("filter", "active", ParameterType.GetOrPost);
        }
	}
}

