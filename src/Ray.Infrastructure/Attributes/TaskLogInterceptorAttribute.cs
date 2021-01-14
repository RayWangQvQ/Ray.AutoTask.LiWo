using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MethodBoundaryAspect.Fody.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ray.Infrastructure.Attributes
{
    public class TaskLogInterceptorAttribute : OnMethodBoundaryAspect
    {
        private readonly ILogger _logger;
        private readonly string _taskName;
        private readonly bool _rethrowWhenException;

        public TaskLogInterceptorAttribute(string taskName = null, bool rethrowWhenException = true)
        {
            _taskName = taskName;
            _rethrowWhenException = rethrowWhenException;

            _logger = Global.ServiceProviderRoot.GetRequiredService<ILogger<TaskLogInterceptorAttribute>>();
        }

        public override void OnEntry(MethodExecutionArgs arg)
        {
            if (_taskName == null) return;

            _logger.LogInformation("---开始【{taskName}】---", _taskName);
        }

        public override void OnExit(MethodExecutionArgs arg)
        {
            if (_taskName == null) return;

            _logger.LogInformation("---结束---\r\n");
        }

        public override void OnException(MethodExecutionArgs arg)
        {
            if (_rethrowWhenException)
            {
                _logger.LogError("程序发生异常：{msg}", arg.Exception.Message);
                base.OnException(arg);
                return;
            }

            _logger.LogError("{task}失败，继续其他任务。失败信息:{msg}\r\n", _taskName, arg.Exception.Message);
            arg.FlowBehavior = FlowBehavior.Continue;
        }
    }
}
