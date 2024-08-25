from django.urls import path

from .views import AboutPageView, HomePageView, TermsConditionsPageView, PrivacyPolicyPageView

urlpatterns = [
    path("", HomePageView.as_view(), name="home"),
    path("about/", AboutPageView.as_view(), name="about"),
    path("terms-conditions/", TermsConditionsPageView.as_view(), name="terms_conditions"),
    path("privacy-policy/", PrivacyPolicyPageView.as_view(), name="privacy_policy"),
]