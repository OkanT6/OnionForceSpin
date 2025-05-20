using OnionForceSpin.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Auth.Exceptions
{
    public class UserAlreadyExistsException:BaseExceptions
    {
        public UserAlreadyExistsException():base("User already exists!") { }
    }
}
