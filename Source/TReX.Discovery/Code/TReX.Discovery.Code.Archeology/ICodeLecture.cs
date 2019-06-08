<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Text;
=======
﻿using CSharpFunctionalExtensions;
>>>>>>> 5f3ed5b0972aae99e89a4fee1f8c388cad4359df
using TReX.Discovery.Code.Domain;

namespace TReX.Discovery.Code.Archeology
{
<<<<<<< HEAD
    public interface ICodeLecture
    {
        CodeResource ToCodeLecture();
    }
}
=======
    internal interface ICodeLecture
    {
        Result<CodeResource> ToResource();
    }
}
>>>>>>> 5f3ed5b0972aae99e89a4fee1f8c388cad4359df
