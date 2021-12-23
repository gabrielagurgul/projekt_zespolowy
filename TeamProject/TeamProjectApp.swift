//
//  TeamProjectApp.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 05/12/2021.
//

import SwiftUI

@main
struct TeamProjectApp: App {
    var body: some Scene {
        WindowGroup {
//			CategoryDetailView(viewModel: CategoryDetailViewModel(budgetType: BudgetType(id: 1, type: "Food", budget: nil)))
			SalaryView()
        }
    }
}
