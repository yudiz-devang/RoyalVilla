redirectToCheckOut = function (sessionId) {
   var stripe = Stripe('pk_test_51MKgrnSDvojLMnVX9erbTNwcYvhwitJoiMoWvNTz7iiNk6YGOslLjoQVu8fouc1vZFiUScIMLOCokxHbFIddrIp2006GYSaHSU');
    stripe.redirectToCheckOut({
        sessionId: sessionId
    });
}