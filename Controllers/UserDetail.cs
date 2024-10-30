using GraphQL;
using GraphQL.Types;
using GraphQlApi.Repository.Interfaces;

namespace GraphQlApi.Controllers;

public class UserDetail
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
public class UserDetailType : ObjectGraphType<UserDetail>
{
    public UserDetailType()
    {
        Field(x => x.Id);
        Field(x => x.FirstName);
        Field(x => x.LastName);
        Field(x => x.Email);
        Field(x => x.Phone);
    }
}

public class UserQuery : ObjectGraphType
{
    public UserQuery(IUserService userService)
    {
        Field<ListGraphType<UserDetailType>>(
            name: "users", // Use lowercase for consistency
            resolve: context => userService.GetUsers()
        );

        Field<UserDetailType>(
            name: "user",
            arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "Id" }), // Use lowercase for argument name
            resolve: context => userService.GetUser(context.GetArgument<int>("Id"))
        );
    }
}

public class UserDetailsSchema : Schema
{
    public UserDetailsSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<UserQuery>();
        Mutation = serviceProvider.GetRequiredService<UserMutation>();
    }
}


//create

public class UserDetailInputType : InputObjectGraphType
{
    public UserDetailInputType()
    {
        Name = "UserDetailInput";
        Field<NonNullGraphType<StringGraphType>>("firstName");
        Field<NonNullGraphType<StringGraphType>>("lastName");
        Field<NonNullGraphType<StringGraphType>>("email");
        Field<StringGraphType>("phone");
    }
}

public class UserMutation : ObjectGraphType
{
    public UserMutation(IUserService userService)
    {
        FieldAsync<UserDetailType>(
            "createUser",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserDetailInputType>> { Name = "user" }),
            resolve: async context =>
            {
                var userDetail = context.GetArgument<UserDetail>("user");
                return await userService.Create(userDetail);
            }
        );
    }
}


//-------------------------------------Sample query --------------------------------------------------
//mutation {
//  createUser(user: {
//firstName: "vivek",
//    lastName: "ss",
//    email: "john.doe@example0a.com",
//    phone: "123-456-7890"
//  }) {
//    id
//    firstName
//    lastName
//    email
//    phone
//  }
//}
