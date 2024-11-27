using CommunityToolkit.Mvvm.Messaging.Messages;
using MauiRecipes.MVVM.Models;

namespace MauiRecipes.Messaging
{
    public class SavedSearchesUpdatedMessage : ValueChangedMessage<List<SavedSearches>>
    {
        public SavedSearchesUpdatedMessage(List<SavedSearches> savedSearches)
            : base(savedSearches) { }
    }
}
