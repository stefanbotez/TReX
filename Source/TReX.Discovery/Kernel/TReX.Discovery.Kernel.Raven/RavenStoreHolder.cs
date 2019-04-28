using System;
using CSharpFunctionalExtensions;
using EnsureThat;
using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using TReX.Discovery.Kernel.Shared;

namespace TReX.Discovery.Kernel.Raven
{
    public sealed class RavenStoreHolder
    {
        private readonly RavenSettings settings;
        private readonly Lazy<IDocumentStore> store;

        public RavenStoreHolder(RavenSettings settings)
        {
            EnsureArg.IsNotNull(settings);

            this.settings = settings;
            store = new Lazy<IDocumentStore>(CreateStore);
        }

        public IDocumentStore Store => store.Value;

        private IDocumentStore CreateStore()
        {
            var documentStore = new DocumentStore
            {
                Urls = new[] { settings.ServerUrl },
                Database = settings.DatabaseName
            };

            documentStore.Initialize();
            SetupDatabase(documentStore);

            return documentStore;
        }

        private void SetupDatabase(IDocumentStore store)
        {
            var getDatabase = new GetDatabaseRecordOperation(settings.DatabaseName);
            var dbOrNothing = store.Maintenance.Server.Send(getDatabase).ToMaybe();

            dbOrNothing.ToResult("Database does not exist")
                .OnFailure(() => CreateDatabase(store));
        }

        private void CreateDatabase(IDocumentStore store)
        {
            var dbRecord = new DatabaseRecord(settings.DatabaseName);
            var createDatabase = new CreateDatabaseOperation(dbRecord);

            store.Maintenance.Server.Send(createDatabase);
        }
    }
}