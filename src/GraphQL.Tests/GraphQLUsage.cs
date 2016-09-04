using GraphQL.Next.Configs;
using GraphQL.Next.Resolvers;
using GraphQL.Next.Types;
using GraphQL.StarWars.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL.StarWars
{
 
    public class MyType : GraphQLObjectType
    { 
        public MyType()
        {
            For<Human>()
                .Field(x => x.Name);
        }
    }

    public class StarWarsSchema
    {
        public GraphQLObjectType Query { get; set; }
        public GraphQLObjectType Mutation { get; set; }

        public StarWarsSchema(IStarWarsData data)
        {
            var episodeEnum = GraphQLEnumType.For<Episodes>(_ =>
            {
                _.Name = "Episode";
                _.Description = "One of the films in the Star Wars Trilogy.";
                _.Value("NEWHOPE", Episodes.NEWHOPE, "Released in 1977.");
                _.Value("EMPIRE", Episodes.EMPIRE, "Released in 1980.");
                _.Value("JEDI", Episodes.JEDI, "Released in 1983.");
            });

            var episodeEnum2 = GraphQLEnumType.For<Episodes>();

            var characterType = GraphQLInterfaceType.For<ICharacter>(_ =>
            {
                _.Field(x => x.Id);
                _.Field("name", x => x.Name);
                _.Field("friends", _.Self);
                _.Field("appearsIn", new GraphQLList(episodeEnum));
            });

            var droidType = GraphQLObjectType.For<Droid>(type => type
                .Field(x => x.Name)
                .Field(f => f
                    .Name("friends")
                    .Type(new GraphQLList(characterType))
                    .Resolve(context => data.FriendsFor(context.Source))
                )
                .Interface(characterType)
            );

            var humanType = GraphQLObjectType.For<Human>(_ =>
            {
                _.Field(x => x.Name);
                _.Field(
                    name: "friends",
                    type: new GraphQLList(characterType),
                    resolve: context => data.FriendsForAsync(context.Source));
                _.Interface(characterType);
                _.IsOfType(value => value is Human);
            });

            var inputType = GraphQLInputObjectType.For(_ =>
            {
                _.Field<string>("fred");
            });

            var inputType2 = GraphQLInputObjectType.For<ICharacter>(_ =>
            {
                _.Field(x => x.Id);
            });

            var queryRoot = GraphQLObjectType.For<ICharacter>(_ =>
            {
                _.Name("Root");
                _.Field(
                    "hero",
                    characterType,
                    resolve: context => data.GetDroidByIdAsync("3"));
                //_.Field(
                //    "droid",
                //    new GraphQLList(droidType),
                //    args: args => args.GetArgument<string>("id", "Id of the droid."),
                //    resolve: context => data.GetDroidByIdAsync(context.GetArgument<string>("id"))
                //  );
                _.Field(f => f
                    .Name("human")
                    .Type(new GraphQLList(humanType))
                    //.Argument<GraphQLString>(args => args
                    //    .Name("id")
                    //    .Description("Id of the human."))
                    .Resolve(context => 
                        data.GetHumanByIdAsync(context.GetArgument<string>("id"))
                    )
                );
            });

            Query = queryRoot;
        }
    }
}
