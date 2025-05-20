using OnionForceSpin.Application.Bases;
using OnionForceSpin.Application.Features.Auth.Exceptions;
using OnionForceSpin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Auth.Rules
{
    public class AuthRules:BaseRules
    {
        public Task UserShouldNotBeExisted(User user)
        {
            if(user is not null) throw new UserAlreadyExistsException();
            return Task.CompletedTask;
        }
    }
}
