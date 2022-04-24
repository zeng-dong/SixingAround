using FluentAssertions;
using SelfPacedEventSourcing.DomainEvents;
using System;
using System.Linq;
using Xunit;

namespace SelfPacedEventSourcingTests;

public class EventsDefinitionTests
{
    [Fact]
    [Trait("Category", "SkipCI")]
    public void AllEventTypes_ShouldBeDefined()
    {
        var shoppingCartId = Guid.NewGuid();
        var clientId = Guid.NewGuid();

        var shoesId = Guid.NewGuid();
        var tShirtId = Guid.NewGuid();
        var twoPairsOfShoes =
            new PricedProductItem
            {
                ProductId = shoesId,
                Quantity = 2,
                UnitPrice = 100
            };
        var pairOfShoes =
            new PricedProductItem
            {
                ProductId = shoesId,
                Quantity = 1,
                UnitPrice = 100
            };
        var tShirt =
            new PricedProductItem
            {
                ProductId = tShirtId,
                Quantity = 1,
                UnitPrice = 50
            };

        var events = new object[]
        {
            new ShoppingCartOpened(shoppingCartId, clientId),
            new ProductItemAddedToShoppingCart(shoppingCartId, tShirt),
            new ProductItemRemovedFromShoppingCart(shoppingCartId, pairOfShoes),
            new ShoppingCartConfirmed(shoppingCartId, DateTime.UtcNow),
            new ShoppingCartCanceled(shoppingCartId, DateTime.UtcNow)
        };

        const int expectedEventTypesCount = 5;
        events.Should().HaveCount(expectedEventTypesCount);
        events.GroupBy(e => e.GetType()).Should().HaveCount(expectedEventTypesCount);
    }
}