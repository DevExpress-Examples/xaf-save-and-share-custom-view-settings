<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592707/22.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T537863)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# XAF - How to Save and Share Custom View Settings

In XAF applications, View settings are stored in Model Differences individually for each user. When the user changes the View (e. g., adds a column to a List View or groups View Items in a Detail View layout), these settings are saved in the user's Model Differences and applied to this View the next time it is displayed.  
The built-in [View Variants Module](https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113011.aspx) allows creating multiple predefined view settings using the Model Editor or in code and provides the end user with the capability to select a view variant at runtime.

We want to provide the capability to save customized view settings as new view variants at runtime, share them, and allow any user having the corresponding permission to select a variant and apply these settings.

> **NOTE:**
> The approach demonstrated in this example may be inappropriate or overly complicated for certain use cases. So, we cannot guarantee that it will work in all possible scenarios. Should you face any issue with this solution, please submit a separate ticket and describe your scenario in detail. Our R&D team will research your feedback to improve the functionality.

## Implementation Details

In this example, we use a persistent class to store view settings in the database and a view controller with actions to manage these settings (creating, applying and deleting). Each user can create his/her own view variants. Each view variant can be optionally marked as shared, so that other users can see and apply it to their views.

### Add a Persistent Class to Store View Settings

First, create the `SettingsStore` class with the following properties to store the View settings:  

1. **Xml** - a string where serialized view settings are stored;  

2. **Name** - the name of the View Variant;  

3. **OwnerId** - an identifier of the user who created this Variant;  

4. **IsShared** - specifies whether this Variant is shared with other users, or not; 

5. **ViewId** - an identifier of the View for which this variant is created.

### Add a Custom View Controller

Then, create the `ViewController`that defines the following behavior:  

1. There are Shared Model Settings available for each user, which cannot be edited by them.  

2. Each user has his/her own default settings saved in the user's Model and used when there are no variants.

3. The **SaveAsNewViewVariant** action creates a new View Variant based on current view customizations. If this is the first Variant created for the view, two new Variants are created: a Variant that stores default settings (named "Default") and a Variant that stores customized view settings. If at least one Variant already exists, only the latter View Variant (with current customizations) is created. This variant becomes current. 

4. The **SelectViewVariant** action makes the View Variant selected in the combo box current. This action is available when at least one variant exists. When the current View Variant is changed, customizations applied to the previous View Variant are lost. Only the "Default" View Variant customizations are saved in the Model when the current variant is changed.  

5. The **UpdateCurrentViewVariant** action saves customizations to the currently selected View Variant.  

6. The **DeleteViewVariant** action deletes the current View Variant. After deletion, the "Default" view variant becomes current and its settings are applied.  

7. The **UpdateDefaultViewVariant** action saves customizations made to the current view in the "Default" Variant.

Actions that the **ViewVariantsController** controller implements may look as follows:

![](https://user-images.githubusercontent.com/14300209/225338143-2b4a470c-43ca-405e-83c0-eceb853c3946.png)

This example demonstrates the basic functionality, which you can expand or customize according to your requirements. For example, you can prevent certain users from deleting View Variants using the [Security System](https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113361.aspx) facilities. Also, you can store the current Variant in the Model (see the [Extend and Customize the Application Model in Code](https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113169.aspx) topic in our documentation) or in a property of the user object and apply it when the corresponding View is opened.

## Files to Review

* [SettingsStore.cs](./CS/EFCore/ViewSettingsEF/ViewSettingsEF.Module/BusinessObjects/SettingsStore.cs)
* [ViewVariantsController.cs](./CS/EFCore/ViewSettingsEF/ViewSettingsEF.Module/Controllers/ViewVariantsController.cs)