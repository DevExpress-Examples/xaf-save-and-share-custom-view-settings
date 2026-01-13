<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592707/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T537863)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# XAF - How to Save and Share Custom View Settings

In XAF applications, view settings are stored for each user individually. When a user changes a view (for example, adds a column to a list view or groups a detail view's items), these settings are saved in the user's model differences and applied to this view the next time this view is displayed.  
The built-in [View Variants Module](https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113011.aspx) allows you to create multiple predefined view settings in the Model Editor or in code. It also allows an end user to select a view variant at runtime.

This example implements functionality required to save customized view settings as new view variants at runtime, share them between users, and allow any user who has appropriate permissions to select a variant and apply its settings to a view.

> **NOTE:**
> The approach demonstrated in this example may be inappropriate or overly complex in certain use cases. Thus, we cannot guarantee that it will work in all possible use case scenarios. Do not hesitate to modify it so it handles your business needs.

## Implementation Details

The example application uses a persistent class to store view settings in the database and a view controller with actions used to manage these settings (create, apply, and delete). Each user can create his/her own view variants. Each view variant can be optionally marked as _shared_, so that other users can see this variant in the UI and apply it to their views.

### Add a Persistent Class to Store View Settings

First, create the `SettingsStore` business class used to store the View settings. This class should have the following properties:  

1. `Xml` - A string where serialized view settings are stored.

2. `Name` - The name of the view variant.

3. `OwnerId` - An identifier of the user who created this variant.

4. `IsShared` - Specifies whether this variant is shared with other users. 

5. `ViewId` - An identifier of the view for which this variant is created.

### Add a Custom View Controller

Create a `ViewController` that defines the following behavior:  

1. The shared model settings are available to all users but cannot be edited by them.  

2. Each user has his/her own default settings saved in the user's model and used when no variant is used.

3. The `SaveAsNewViewVariant` action creates a new view variant based on customizations made to a view. The created variant is assigned as the _current_ variant. If this is the first variant created for the view, the action additionally creates a variant that stores the default settings (named "Default").

4. The `SelectViewVariant` action allows a user to select a view variant from a combo box and makes the selected variant _current_. This action is available when at least one variant exists. When a user changes the current view variant, all customizations previously made to the view are lost, except for changes made to the "Default" view variant.  

5. The `UpdateCurrentViewVariant` action saves customizations to the currently selected view variant.  

6. The `DeleteViewVariant` action deletes the current view variant. After deletion, the "Default" view variant becomes _current_ and its settings are applied.  

7. The `UpdateDefaultViewVariant` action saves customizations made to the current view in the "Default" variant.

In the example application, the actions that `ViewVariantsController` implements look as follows:

![View Controller Actions in UI](https://user-images.githubusercontent.com/14300209/225338143-2b4a470c-43ca-405e-83c0-eceb853c3946.png)

You can extend and adjust the demonstrated functionality based on your requirements. For example, you can:

- Use the [Security System](https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113361.aspx) facilities to prohibit certain users from deleting view variants.

- Store the current variant in the model (see the [Change the Application Model](https://docs.devexpress.com/eXpressAppFramework/403527/ui-construction/application-model-ui-settings-storage/change-application-model) topic in our documentation) or in the user object's property and apply it when the corresponding view is opened.

## Files to Review

* [SettingsStore.cs](./CS/EFCore/ViewSettingsEF/ViewSettingsEF.Module/BusinessObjects/SettingsStore.cs)
* [ViewVariantsController.cs](./CS/EFCore/ViewSettingsEF/ViewSettingsEF.Module/Controllers/ViewVariantsController.cs)

## Documentation

* [View Variants (Switch Document Layouts)](https://docs.devexpress.com/eXpressAppFramework/113011/application-shell-and-base-infrastructure/view-variants-module)
* [How the XAF Application Model Works](https://docs.devexpress.com/eXpressAppFramework/112580/ui-construction/application-model-ui-settings-storage/how-application-model-works)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-save-and-share-custom-view-settings&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-save-and-share-custom-view-settings&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
