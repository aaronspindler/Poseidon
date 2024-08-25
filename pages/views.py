from django.views.generic import TemplateView


class HomePageView(TemplateView):
    template_name = "pages/home.html"


class AboutPageView(TemplateView):
    template_name = "pages/about.html"

# Add this new view
class TermsConditionsPageView(TemplateView):
    template_name = "pages/terms_conditions.html"

# Add this new view
class PrivacyPolicyPageView(TemplateView):
    template_name = "pages/privacy_policy.html"